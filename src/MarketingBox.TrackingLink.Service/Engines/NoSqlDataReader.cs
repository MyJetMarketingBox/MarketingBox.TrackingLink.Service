using System.Linq;
using MarketingBox.Affiliate.Service.Domain.Models.Affiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Brands;
using MarketingBox.Affiliate.Service.Domain.Models.OfferAffiliates;
using MarketingBox.Affiliate.Service.Domain.Models.Offers;
using MarketingBox.Affiliate.Service.MyNoSql.Affiliates;
using MarketingBox.Affiliate.Service.MyNoSql.Brands;
using MarketingBox.Affiliate.Service.MyNoSql.Offer;
using MarketingBox.Affiliate.Service.MyNoSql.OfferAffiliates;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.TrackingLink.Service.Engines.Interfaces;
using MyNoSqlServer.Abstractions;

namespace MarketingBox.TrackingLink.Service.Engines
{
    public class NoSqlDataReader : INoSqlDataReader
    {
        private readonly IMyNoSqlServerDataReader<OfferAffiliateNoSql> _noSqlOfferAffiliateReader;
        private readonly IMyNoSqlServerDataReader<OfferNoSql> _noSqlOfferReader;
        private readonly IMyNoSqlServerDataReader<BrandNoSql> _noSqlBrandReader;
        private readonly IMyNoSqlServerDataReader<AffiliateNoSql> _noSqlAffiliateReader;

        public NoSqlDataReader(
            IMyNoSqlServerDataReader<OfferAffiliateNoSql> noSqlOfferAffiliateReader,
            IMyNoSqlServerDataReader<OfferNoSql> noSqlOfferReader,
            IMyNoSqlServerDataReader<BrandNoSql> noSqlBrandReader,
            IMyNoSqlServerDataReader<AffiliateNoSql> noSqlAffiliateReader)
        {
            _noSqlOfferAffiliateReader = noSqlOfferAffiliateReader;
            _noSqlOfferReader = noSqlOfferReader;
            _noSqlBrandReader = noSqlBrandReader;
            _noSqlAffiliateReader = noSqlAffiliateReader;
        }

        public BrandMessage GetBrand(long brandId)
        {
            var brand = _noSqlBrandReader.Get().FirstOrDefault(x => x.Brand.Id == brandId)?.Brand;

            if (brand is null)
            {
                throw new NotFoundException("Brand with id", brandId);
            }

            return brand;
        }

        public Offer GetOffer(long offerId)
        {
            var offer = _noSqlOfferReader.Get().FirstOrDefault(x => x.Offer.Id == offerId)?.Offer;

            if (offer is null)
            {
                throw new NotFoundException("Offer with id", offerId);
            }

            return offer;
        }

        public Offer GetOffer(string uniqueId)
        {
            var offer = _noSqlOfferReader.Get().FirstOrDefault(x => x.UniqueId == uniqueId)?.Offer;

            if (offer is null)
            {
                throw new NotFoundException("Offer with uniqueId", uniqueId);
            }

            return offer;
        }

        public OfferAffiliate GetOfferAffiliate(string uniqueId)
        {
            var offerAffiliate = _noSqlOfferAffiliateReader
                .Get(OfferAffiliateNoSql.GeneratePartitionKey(), uniqueId)?.OfferAffiliate;

            if (offerAffiliate is null)
            {
                throw new NotFoundException("OfferAffiliate with uniqueId", uniqueId);
            }

            return offerAffiliate;
        }

        public AffiliateMessage GetAffiliate(long affiliateId)
        {
            var affiliate = _noSqlAffiliateReader
                .Get()
                .FirstOrDefault(x => x.Affiliate.AffiliateId == affiliateId)?.Affiliate;

            if (affiliate is null)
            {
                throw new NotFoundException("Affiliate with Id", affiliateId);
            }

            return affiliate;
        }
    }
}