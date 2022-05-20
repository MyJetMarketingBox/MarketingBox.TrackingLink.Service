using System.Threading.Tasks;
using JetBrains.Annotations;
using MarketingBox.Affiliate.Service.Client.Interfaces;
using MarketingBox.Affiliate.Service.Domain.Models.Affiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Brands;
using MarketingBox.Affiliate.Service.Domain.Models.OfferAffiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Offers;
using MarketingBox.TrackingLink.Service.Engines.Interfaces;

namespace MarketingBox.TrackingLink.Service.Engines
{
    public class NoSqlDataReader : INoSqlDataReader
    {
        private readonly IOfferClient _offerClient;
        private readonly IOfferAffiliateClient _offerAffiliateClient;
        private readonly IAffiliateClient _affiliateClient;
        private readonly IBrandClient _brandClient;

        public NoSqlDataReader(IOfferClient offerClient,
            IOfferAffiliateClient offerAffiliateClient,
            IAffiliateClient affiliateClient, IBrandClient brandClient)
        {
            _offerClient = offerClient;
            _offerAffiliateClient = offerAffiliateClient;
            _affiliateClient = affiliateClient;
            _brandClient = brandClient;
        }

        public async Task<BrandMessage> GetBrand(long brandId)
        {
            var brand = await _brandClient.GetBrandById(brandId);

            return brand;
        }

        public async Task<Offer> GetOffer(long offerId)
        {
            var offer = await _offerClient.GetOfferByTenantAndId(offerId);

            return offer;
        }

        public async Task<Offer> GetOffer(string uniqueId)
        {
            var offer = await _offerClient.GetOfferByUniqueId(uniqueId);

            return offer;
        }

        public async Task<OfferAffiliate> GetOfferAffiliate(string uniqueId)
        {
            var offerAffiliate = await _offerAffiliateClient.GetOfferAffiliateByUniqueId(uniqueId);

            return offerAffiliate;
        }

        [ItemCanBeNull]
        public async Task<AffiliateMessage> GetAffiliate(long affiliateId)
        {
            var affiliate = await _affiliateClient.GetAffiliateById(affiliateId);

            return affiliate;
        }
    }
}