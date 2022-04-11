using System.Runtime.Serialization;

namespace MarketingBox.TrackingLink.Service.Domain.Models
{
    [DataContract]
    public class TrackingLink
    {
        [DataMember(Order = 1)] public long Id { get; set; }
        [DataMember(Order = 2)] public long ClickId { get; set; }
        [DataMember(Order = 3)] public long BrandId { get; set; }
        [DataMember(Order = 4)] public long AffiliateId { get; set; }
        [DataMember(Order = 5)] public string Link { get; set; }
        [DataMember(Order = 6)] public LinkParameters LinkParameterValues { get; set; }
        [DataMember(Order = 7)] public LinkParameters LinkParameterNames { get; set; }
        [DataMember(Order = 8)] public string UniqueId { get; set; }
    }
}