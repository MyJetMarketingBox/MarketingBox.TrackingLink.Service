using System.Linq;
using MarketingBox.Affiliate.Service.Domain.Models.Brands;
using MarketingBox.Affiliate.Service.Domain.Models.OfferAffiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Offers;
using MarketingBox.Affiliate.Service.MyNoSql.Brands;
using MarketingBox.Affiliate.Service.MyNoSql.Offer;
using MarketingBox.Affiliate.Service.MyNoSql.OfferAffiliates;
using MarketingBox.TrackingLink.Service.Engines.Interfaces;
using MyNoSqlServer.Abstractions;

namespace MarketingBox.TrackingLink.Service.Engines
{
    public class NoSqlDataReader : INoSqlDataReader
    {
        private readonly IMyNoSqlServerDataReader<OfferAffiliateNoSql> _noSqlOfferAffiliateReader;
        private readonly IMyNoSqlServerDataReader<OfferNoSql> _noSqlOfferReader;
        private readonly IMyNoSqlServerDataReader<BrandNoSql> _noSqlBrandReader;

        public NoSqlDataReader(
            IMyNoSqlServerDataReader<OfferAffiliateNoSql> noSqlOfferAffiliateReader,
            IMyNoSqlServerDataReader<OfferNoSql> noSqlOfferReader,
            IMyNoSqlServerDataReader<BrandNoSql> noSqlBrandReader)
        {
            _noSqlOfferAffiliateReader = noSqlOfferAffiliateReader;
            _noSqlOfferReader = noSqlOfferReader;
            _noSqlBrandReader = noSqlBrandReader;
        }

        public BrandMessage GetBrand(long id)
        {
            var brandNoSql = _noSqlBrandReader.Get().FirstOrDefault(x => x.Brand.Id == id);
            return brandNoSql?.Brand;
        }
        public Offer GetOffer(long id)
        {
            var offerNoSql = _noSqlOfferReader.Get().FirstOrDefault(x => x.Offer.Id == id);
            return offerNoSql?.Offer;
        }
        public OfferAffiliate GetOfferAffiliate(string uniqueId)
        {
            var offerAffiliateNoSql = _noSqlOfferAffiliateReader.Get(OfferNoSql.GeneratePartitionKey(), uniqueId);
            return offerAffiliateNoSql?.OfferAffiliate;
        }
    }
}