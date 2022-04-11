using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MarketingBox.TrackingLink.Service.Domain.Models
{
    [DataContract]
    public class LinkParameters
    {
        [DataMember(Order = 1), StringLength(20, MinimumLength = 1)]
        public string ClickId { get; set; }

        [DataMember(Order = 2), StringLength(20, MinimumLength = 1)]
        public string Language { get; set; }

        [DataMember(Order = 3), StringLength(20, MinimumLength = 1)]
        public string MPC_1 { get; set; }

        [DataMember(Order = 4), StringLength(20, MinimumLength = 1)]
        public string MPC_2 { get; set; }

        [DataMember(Order = 5), StringLength(20, MinimumLength = 1)]
        public string MPC_3 { get; set; }

        [DataMember(Order = 6), StringLength(20, MinimumLength = 1)]
        public string MPC_4 { get; set; }
    }
}