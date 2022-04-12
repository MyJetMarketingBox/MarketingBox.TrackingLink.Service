using Autofac;
using MarketingBox.TrackingLink.Service.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.TrackingLink.Service.Client
{
    public static class AutofacHelper
    {
        public static void ServiceClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new TrackingLinkServiceClientFactory(grpcServiceUrl);
            builder
                .RegisterInstance(factory.GetTrackingLinkService())
                .As<ITrackingLinkService>()
                .SingleInstance();
        }
    }
}
