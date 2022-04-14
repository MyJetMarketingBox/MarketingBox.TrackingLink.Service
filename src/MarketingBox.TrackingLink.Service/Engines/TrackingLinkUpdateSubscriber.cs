using System;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using MarketingBox.Postback.Service.Messages;
using MarketingBox.Sdk.Common.Extensions;
using MarketingBox.TrackingLink.Service.Grpc.Requests;
using MarketingBox.TrackingLink.Service.Messages;
using MarketingBox.TrackingLink.Service.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;

namespace MarketingBox.TrackingLink.Service.Engines
{
    public class TrackingLinkUpdateSubscriber : IStartable
    {
        private readonly IServiceBusPublisher<TrackingLinkUpsertMessage> _publisherTrackingLink;
        private readonly ILogger<TrackingLinkUpdateSubscriber> _logger;
        private readonly ITrackingLinkRepository _repository;

        public TrackingLinkUpdateSubscriber(
            ISubscriber<TrackingLinkUpdateRegistrationIdMessage> subscriber,
            IServiceBusPublisher<TrackingLinkUpsertMessage> publisherTrackingLink,
            ITrackingLinkRepository repository,
            ILogger<TrackingLinkUpdateSubscriber> logger)
        {
            _publisherTrackingLink = publisherTrackingLink;
            _repository = repository;
            _logger = logger;
            subscriber.Subscribe(Handle);
        }

        private async ValueTask Handle(TrackingLinkUpdateRegistrationIdMessage message)
        {
            try
            {
                _logger.LogInformation("Processing message {@Context}", message);

                var request = new TrackingLinkUpdateRegistrationIdRequest
                {
                    ClickId = message.ClickId,
                    RegistrationId = message.RegistrationId
                };
                
                request.ValidateEntity();

                var trackingLink = await _repository.UpdateRegistrationIdAsync(request);
                
                await _publisherTrackingLink.PublishAsync(new TrackingLinkUpsertMessage {TrackingLink = trackingLink});
                
                _logger.LogInformation("Message {@Context} was processed", message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error during processing {@Context}", message);
                throw;
            }
        }

        public void Start()
        {
        }
    }
}