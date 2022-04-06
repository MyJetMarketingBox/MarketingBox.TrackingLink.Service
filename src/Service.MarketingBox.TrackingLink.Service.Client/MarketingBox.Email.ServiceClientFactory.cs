using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;

namespace Service.MarketingBox.TrackingLink.Service.Client
{
    [UsedImplicitly]
    public class ServiceClientFactory : MyGrpcClientFactory
    {
        public ServiceClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }
    }
}
