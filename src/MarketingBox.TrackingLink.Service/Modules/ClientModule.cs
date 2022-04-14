using Autofac;
using MarketingBox.Postback.Service.Messages;
using MarketingBox.TrackingLink.Service.Messages;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;

namespace MarketingBox.TrackingLink.Service.Modules
{
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder
                .RegisterMyServiceBusTcpClient(
                    Program.ReloadedSettings(e => e.MarketingBoxServiceBusHostPort),
                    Program.LogFactory);
            builder.RegisterMyServiceBusPublisher<TrackingLinkUpsertMessage>(
                serviceBusClient, TrackingLinkUpsertMessage.Topic, false);
            
            const string queueName = "marketingbox-trackinglink-service";
            builder.RegisterMyServiceBusSubscriberSingle<TrackingLinkUpdateRegistrationIdMessage>(
                serviceBusClient,
                TrackingLinkUpdateRegistrationIdMessage.Topic,
                queueName,
                TopicQueueType.PermanentWithSingleConnection);
        }
    }
}