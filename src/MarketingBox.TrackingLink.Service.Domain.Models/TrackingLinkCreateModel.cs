namespace MarketingBox.TrackingLink.Service.Domain.Models
{
    public class TrackingLinkCreateModel
    {
        public string Link { get; set; }
        public long AffiliateId { get; set; }
        public long BrandId { get; set; }
        public LinkParameterValues LinkParameterValuesValues { get; set; }
        public string UniqueId { get; set; }
        public LinkParameterNames LinkParameterNames { get; set; }
    }
}