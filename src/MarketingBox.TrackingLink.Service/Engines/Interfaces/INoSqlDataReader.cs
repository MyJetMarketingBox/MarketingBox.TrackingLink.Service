using MarketingBox.Affiliate.Service.Domain.Models.Brands;
using MarketingBox.Affiliate.Service.Domain.Models.OfferAffiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Offers;

namespace MarketingBox.TrackingLink.Service.Engines.Interfaces
{
    public interface INoSqlDataReader
    {
        BrandMessage GetBrand(long id);
        Offer GetOffer(long id);
        OfferAffiliate GetOfferAffiliate(string uniqueId);
    }
}