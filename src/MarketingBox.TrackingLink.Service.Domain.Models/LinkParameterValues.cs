using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.TrackingLink.Service.Domain.Models
{
    [DataContract]
    public class LinkParameterValues : ValidatableEntity
    {
        [DataMember(Order = 1), StringLength(50, MinimumLength = 1)]
        public string Language { get; set; }

        [DataMember(Order = 2), StringLength(50, MinimumLength = 1)]
        public string MPC_1 { get; set; }

        [DataMember(Order = 3), StringLength(50, MinimumLength = 1)]
        public string MPC_2 { get; set; }

        [DataMember(Order = 4), StringLength(50, MinimumLength = 1)]
        public string MPC_3 { get; set; }

        [DataMember(Order = 5), StringLength(50, MinimumLength = 1)]
        public string MPC_4 { get; set; }
    }
}