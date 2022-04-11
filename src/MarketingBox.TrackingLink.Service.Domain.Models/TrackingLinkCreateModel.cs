namespace MarketingBox.TrackingLink.Service.Domain.Models
{
    public class TrackingLinkCreateModel
    {
       public string Link {get;set;}
       public long AffiliateId {get;set;}
       public long BrandId {get;set;}
       public LinkParameters LinkParameterValues {get;set;}
       public string UniqueId {get;set;}
       public LinkParameters LinkParameterNames { get; set; }
    }
}