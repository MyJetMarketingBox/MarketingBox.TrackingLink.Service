using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Attributes;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.TrackingLink.Service.Grpc.Requests;

[DataContract]
public class TrackingLinkUpdateRegistrationIdRequest : ValidatableEntity
{
    [DataMember(Order = 1), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
    public long? ClickId { get; set; }
    
    [DataMember(Order = 2), Required, AdvancedCompare(ComparisonType.GreaterThan, 0)]
    public long? RegistrationId { get; set; }
}