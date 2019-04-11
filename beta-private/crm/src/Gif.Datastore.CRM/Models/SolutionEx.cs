#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gif.Service.Models
{
  /// <summary>
  /// An Extended Solution with its corresponding Technical Contacts, ClaimedCapability, ClaimedStandard et al
  /// </summary>
  [DataContract]
    public class SolutionEx : EntityBase
    {
        /// <summary>
        /// Solution
        /// </summary>
        /// <value>Solution</value>
        [DataMember(Name = "solution")]
        public Solution Solution { get; set; }

        /// <summary>
        /// A list of ClaimedCapability
        /// </summary>
        /// <value>A list of ClaimedCapability</value>
        [FromBody]
        [DataMember(Name = "claimedCapability")]
        public List<CapabilityImplemented> ClaimedCapability { get; set; }

        /// <summary>
        /// A list of ClaimedCapabilityEvidence
        /// </summary>
        /// <value>A list of ClaimedCapabilityEvidence</value>
        [FromBody]
        [DataMember(Name = "claimedCapabilityEvidence")]
        public List<CapabilityEvidence> ClaimedCapabilityEvidence { get; set; }

        /// <summary>
        /// A list of ClaimedCapabilityReview
        /// </summary>
        /// <value>A list of ClaimedCapabilityReview</value>
        [FromBody]
        [DataMember(Name = "claimedCapabilityReview")]
        public List<Review> ClaimedCapabilityReview { get; set; }

        /// <summary>
        /// A list of ClaimedStandard
        /// </summary>
        /// <value>A list of ClaimedStandard</value>
        [FromBody]
        [DataMember(Name = "claimedStandard")]
        public List<StandardApplicable> ClaimedStandard { get; set; }

        /// <summary>
        /// A list of ClaimedStandardEvidence
        /// </summary>
        /// <value>A list of ClaimedStandardEvidence</value>
        [FromBody]
        [DataMember(Name = "claimedStandardEvidence")]
        public List<StandardApplicableEvidence> ClaimedStandardEvidence { get; set; }

        /// <summary>
        /// A list of ClaimedStandardReview
        /// </summary>
        /// <value>A list of ClaimedStandardReview</value>
        /// A list of TechnicalContact
        [FromBody]
        [DataMember(Name = "claimedStandardReview")]
        public List<Review> ClaimedStandardReview { get; set; }

        /// <summary>
        /// A list of TechnicalContact
        /// </summary>
        /// <value>A list of TechnicalContact</value>
        [FromBody]
        [DataMember(Name = "technicalContact")]
        public List<TechnicalContact> TechnicalContact { get; set; }
    }
}
#pragma warning restore 1591
