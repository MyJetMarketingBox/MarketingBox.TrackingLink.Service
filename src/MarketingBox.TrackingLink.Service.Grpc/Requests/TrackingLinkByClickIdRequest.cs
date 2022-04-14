using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;

namespace MarketingBox.TrackingLink.Service.Grpc.Requests;

[DataContract]
public class TrackingLinkByClickIdRequest
{
    [DataMember(Order = 1), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
    public long? ClickId { get; set; }
}