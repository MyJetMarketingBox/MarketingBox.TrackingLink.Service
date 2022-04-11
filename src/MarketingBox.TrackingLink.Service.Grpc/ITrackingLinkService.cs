using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Sdk.Common.Models.Grpc;
using MarketingBox.TrackingLink.Service.Grpc.Requests;

namespace MarketingBox.TrackingLink.Service.Grpc
{
    [ServiceContract]
    public interface ITrackingLinkService
    {
        [OperationContract]
        Task<Response<string>> CreateAsync(TrackingLinkCreateRequest request);
    }
}