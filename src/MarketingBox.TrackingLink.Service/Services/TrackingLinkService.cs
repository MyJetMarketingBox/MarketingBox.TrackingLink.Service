using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MarketingBox.Affiliate.Service.MyNoSql.OfferAffiliates;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.Sdk.Common.Models.Grpc;
using MarketingBox.TrackingLink.Service.Domain.Models;
using MarketingBox.TrackingLink.Service.Grpc;
using MarketingBox.TrackingLink.Service.Grpc.Requests;
using MarketingBox.TrackingLink.Service.Repositories.Interfaces;
using MyNoSqlServer.Abstractions;

namespace MarketingBox.TrackingLink.Service.Services
{
    public class TrackingLinkService : ITrackingLinkService
    {
        private readonly IMyNoSqlServerDataReader<OfferAffiliateNoSql> _noSqlServerDataReader;
        private readonly ITrackingLinkRepository _repository;
        private readonly IMapper _mapper;

        public TrackingLinkService(IMyNoSqlServerDataReader<OfferAffiliateNoSql> noSqlServerDataReader,
            ITrackingLinkRepository repository, IMapper mapper)
        {
            _noSqlServerDataReader = noSqlServerDataReader;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateAsync(TrackingLinkCreateRequest request)
        {
            try
            {
                request.ValidateEntity();
                
                var offerAffiliateNoSql = _noSqlServerDataReader.Get(
                    OfferAffiliateNoSql.GeneratePartitionKey(),
                    OfferAffiliateNoSql.GenerateRowKey(request.UniqueId));
                if (offerAffiliateNoSql is null)
                {
                    throw new NotFoundException($"OfferAffiliate with {nameof(request.UniqueId)}", request.UniqueId);
                }

                var trackingLink = await _repository.CreateAsync(new()
                {
                    Link = offerAffiliateNoSql.Brand.Link,
                    AffiliateId = offerAffiliateNoSql.OfferAffiliate.AffiliateId,
                    BrandId = offerAffiliateNoSql.Brand.Id,
                    LinkParameterValues = request.LinkParameterValues,
                    LinkParameterNames = _mapper.Map<LinkParameters>(offerAffiliateNoSql.Brand.LinkParameters),
                    UniqueId = request.UniqueId
                });
                var url = BuildUrl(trackingLink);
                return new Response<string>()
                {
                    Status = ResponseStatus.Ok,
                    Data = url
                };
            }
            catch (Exception e)
            {
                return e.FailedResponse<string>();
            }
        }

        private static string BuildUrl(Domain.Models.TrackingLink trackingLink)
        {
            var builder = new StringBuilder();
            builder.Append(trackingLink.Link);
            AppendParameter(
                trackingLink.LinkParameterNames.Language,
                trackingLink.LinkParameterValues.Language,
                builder);
            AppendParameter(
                trackingLink.LinkParameterNames.ClickId,
                trackingLink.LinkParameterValues.ClickId,
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
            
            builder.Append("?");
            builder.Append(HttpUtility.UrlEncode(parameterName));
            builder.Append("=");
            builder.Append(HttpUtility.UrlEncode(parameterValue));
            builder.Append("&");
        }
    }
}