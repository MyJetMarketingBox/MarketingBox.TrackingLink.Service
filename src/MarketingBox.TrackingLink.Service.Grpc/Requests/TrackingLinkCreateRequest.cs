using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Models;
using MarketingBox.TrackingLink.Service.Domain.Models;

namespace MarketingBox.TrackingLink.Service.Grpc.Requests;

[DataContract]
public class TrackingLinkCreateRequest : ValidatableEntity
{
    [DataMember(Order = 1), Required] public string UniqueId { get; set; }
    [DataMember(Order = 2)] public LinkParameters LinkParameterValues { get; set; }
}