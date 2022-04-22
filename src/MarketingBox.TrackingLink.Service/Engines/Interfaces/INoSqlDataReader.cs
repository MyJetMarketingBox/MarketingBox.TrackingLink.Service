using MarketingBox.Affiliate.Service.Domain.Models.Affiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Brands;
using MarketingBox.Affiliate.Service.Domain.Models.OfferAffiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Offers;

namespace MarketingBox.TrackingLink.Service.Engines.Interfaces
{
    public interface INoSqlDataReader
    {
        BrandMessage GetBrand(long brandId);
        Offer GetOffer(long offerId);
        Offer GetOffer(string uniqueId);
        OfferAffiliate GetOfferAffiliate(string uniqueId);
        AffiliateMessage GetAffiliate(long affiliateId);
    }
}