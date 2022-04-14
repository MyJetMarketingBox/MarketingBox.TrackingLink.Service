using System.Threading.Tasks;
using MarketingBox.TrackingLink.Service.Domain.Models;
using MarketingBox.TrackingLink.Service.Grpc.Requests;

namespace MarketingBox.TrackingLink.Service.Repositories.Interfaces
{
    public interface ITrackingLinkRepository
    {
        Task<Domain.Models.TrackingLink> CreateAsync(TrackingLinkCreateModel request);
        Task<Domain.Models.TrackingLink> GetAsync(TrackingLinkByClickIdRequest request);
        Task<Domain.Models.TrackingLink> UpdateRegistrationIdAsync(TrackingLinkUpdateRegistrationIdRequest request);
    }
}