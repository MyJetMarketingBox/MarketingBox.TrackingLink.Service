using AutoMapper;
using MarketingBox.TrackingLink.Service.Domain.Models;
using LinkParameters = MarketingBox.Affiliate.Service.Domain.Models.Brands.LinkParameters;

namespace MarketingBox.TrackingLink.Service.MapperProfiles
{
    public class TrackingLinkMapperProfile : Profile
    {
        public TrackingLinkMapperProfile()
        {
            CreateMap<TrackingLinkCreateModel, Domain.Models.TrackingLink>();
            CreateMap<LinkParameters, Domain.Models.LinkParameters>();
        }
    }
}