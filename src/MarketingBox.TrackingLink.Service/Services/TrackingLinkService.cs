using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MarketingBox.Affiliate.Service.Domain.Models.Offers;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using MarketingBox.TrackingLink.Service.Domain.Models;
using MarketingBox.TrackingLink.Service.Engines.Interfaces;
using MarketingBox.TrackingLink.Service.Grpc;
using MarketingBox.TrackingLink.Service.Grpc.Requests;
using MarketingBox.TrackingLink.Service.Messages;
using MarketingBox.TrackingLink.Service.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;

namespace MarketingBox.TrackingLink.Service.Services
{
    public class TrackingLinkService : ITrackingLinkService
    {
        private readonly ITrackingLinkRepository _repository;
        private readonly IMapper _mapper;
        private readonly INoSqlDataReader _noSqlDataReader;
        private readonly IServiceBusPublisher<TrackingLinkUpsertMessage> _publisherTrackingLink;
        private ILogger<TrackingLinkService> _logger;

        public TrackingLinkService(
            ITrackingLinkRepository repository,
            IMapper mapper,
            INoSqlDataReader noSqlDataReader,
            IServiceBusPublisher<TrackingLinkUpsertMessage> publisherTrackingLink, ILogger<TrackingLinkService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _noSqlDataReader = noSqlDataReader;
            _publisherTrackingLink = publisherTrackingLink;
            _logger = logger;
        }

        public async Task<Response<string>> CreateAsync(TrackingLinkCreateRequest request)
        {
            try
            {
                request.ValidateEntity();
                
                Offer offer;
                long affiliateId;
                var strAffiliateId = request.UniqueId[32..];
                var uniqueId = request.UniqueId[..32];
                if (!string.IsNullOrEmpty(strAffiliateId))
                {
                    _logger.LogInformation("Processing public link for affiliate {strAffiliateId}", strAffiliateId);
                    offer = _noSqlDataReader.GetOffer(uniqueId);

                    if (offer.Privacy == OfferPrivacy.Private)
                    {
                        throw new ForbiddenException("Offer is private now, you can't use public link.");
                    }

                    if (!long.TryParse(strAffiliateId, out affiliateId))
                    {
                        throw new BadRequestException("Incorrect format of affiliateId");
                    }

                    _noSqlDataReader.GetAffiliate(affiliateId);
                }
                else
                {
                    _logger.LogInformation("Processing private link");
                    var offerAffiliate = _noSqlDataReader.GetOfferAffiliate(request.UniqueId);
                    
                    affiliateId = offerAffiliate.AffiliateId;
                    
                    offer = _noSqlDataReader.GetOffer(offerAffiliate.OfferId);
                }
                
                var brand = _noSqlDataReader.GetBrand(offer.BrandId);

                var trackingLink = await _repository.CreateAsync(new()
                {
                    Link = brand.Link,
                    AffiliateId = affiliateId,
                    BrandId = brand.Id,
                    LinkParameterValues = request.LinkParameterValues,
                    LinkParameterNames = _mapper.Map<LinkParameterNames>(brand.LinkParameters),
                    UniqueId = uniqueId
                });

                _logger.LogInformation("Sending message to service bus: {@context}", trackingLink);

                await _publisherTrackingLink.PublishAsync(new TrackingLinkUpsertMessage
                {
                    TrackingLink = trackingLink
                });
                _logger.LogInformation("Message was sent.");

                var url = BuildUrl(trackingLink);
                _logger.LogInformation("Url was built: {@url}", url);
                return new Response<string>
                {
                    Status = ResponseStatus.Ok,
                    Data = url
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e,@"Error occurred while processing request {@Request}",request);
                return e.FailedResponse<string>();
            }
        }

        private static string BuildUrl(Domain.Models.TrackingLink trackingLink)
        {
            var builder = new StringBuilder();
            builder.Append(trackingLink.Link);
            builder.Append("?");
            AppendParameter(
                trackingLink.LinkParameterNames.Language,
                trackingLink.LinkParameterValues.Language,
                builder);
            AppendParameter(
                trackingLink.LinkParameterNames.ClickId,
                trackingLink.ClickId.ToString(),
                builder);
            AppendParameter(
                trackingLink.LinkParameterNames.MPC_1,
                trackingLink.LinkParameterValues.MPC_1,
                builder);
            AppendParameter(
                trackingLink.LinkParameterNames.MPC_2,
                trackingLink.LinkParameterValues.MPC_2,
                builder);
            AppendParameter(
                trackingLink.LinkParameterNames.MPC_3,
                trackingLink.LinkParameterValues.MPC_3,
                builder);
            AppendParameter(
                trackingLink.LinkParameterNames.MPC_4,
                trackingLink.LinkParameterValues.MPC_4,
                builder);

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        private static void AppendParameter(
            string parameterName,
            string parameterValue,
            StringBuilder builder)
        {
            if (string.IsNullOrEmpty(parameterName) || string.IsNullOrEmpty(parameterValue)) return;

            builder.Append(HttpUtility.UrlEncode(parameterName));
            builder.Append("=");
            builder.Append(HttpUtility.UrlEncode(parameterValue));
            builder.Append("&");
        }
    }
}