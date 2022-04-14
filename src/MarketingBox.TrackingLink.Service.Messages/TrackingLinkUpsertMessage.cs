using System.Runtime.Serialization;

namespace MarketingBox.TrackingLink.Service.Messages;

[DataContract]
public class TrackingLinkUpsertMessage
{
    public const string Topic = "marketing-box-trackinglink-service-trackinglink-upsert";
    
    [DataMember(Order = 1)]
    public Domain.Models.TrackingLink TrackingLink { get; set; }
}