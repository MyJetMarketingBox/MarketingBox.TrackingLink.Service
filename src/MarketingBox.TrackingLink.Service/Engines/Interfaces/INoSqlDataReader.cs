using System.Threading.Tasks;
using MarketingBox.Affiliate.Service.Domain.Models.Affiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Brands;
using MarketingBox.Affiliate.Service.Domain.Models.OfferAffiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Offers;

namespace MarketingBox.TrackingLink.Service.Engines.Interfaces
{
    public interface INoSqlDataReader
    {
        Task<BrandMessage> GetBrand(long brandId);
        Task<Offer> GetOffer(long offerId);
        Task<Offer> GetOffer(string uniqueId);
        Task<OfferAffiliate> GetOfferAffiliate(string uniqueId);
        Task<AffiliateMessage> GetAffiliate(long affiliateId);
    }
}