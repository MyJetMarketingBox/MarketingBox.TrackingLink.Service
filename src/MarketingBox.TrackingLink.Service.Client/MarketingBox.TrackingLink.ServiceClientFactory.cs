using JetBrains.Annotations;
using MarketingBox.TrackingLink.Service.Grpc;
using MyJetWallet.Sdk.Grpc;

namespace MarketingBox.TrackingLink.Service.Client
{
    [UsedImplicitly]
    public class TrackingLinkServiceClientFactory : MyGrpcClientFactory
    {
        public TrackingLinkServiceClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }
        public ITrackingLinkService GetTrackingLinkService() => CreateGrpcService<ITrackingLinkService>();
    }
}
