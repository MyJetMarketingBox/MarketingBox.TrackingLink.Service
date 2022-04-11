using System.Threading.Tasks;
using MarketingBox.TrackingLink.Service.Domain.Models;

namespace MarketingBox.TrackingLink.Service.Repositories.Interfaces
{
    public interface ITrackingLinkRepository
    {
        Task<Domain.Models.TrackingLink> CreateAsync(TrackingLinkCreateModel request);
    }
}