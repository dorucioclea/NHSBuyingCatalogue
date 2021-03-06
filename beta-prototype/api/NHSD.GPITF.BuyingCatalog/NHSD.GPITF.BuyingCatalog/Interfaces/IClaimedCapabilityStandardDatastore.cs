﻿using NHSD.GPITF.BuyingCatalog.Models;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Interfaces
{
#pragma warning disable CS1591
  public interface IClaimedCapabilityStandardDatastore
  {
    IQueryable<ClaimedCapabilityStandard> ByClaimedCapability(string claimedCapabilityId);
    IQueryable<ClaimedCapabilityStandard> ByStandard(string standardId);
    ClaimedCapabilityStandard Create(ClaimedCapabilityStandard claimedCapStd);
    void Delete(ClaimedCapabilityStandard claimedCapStd);
    void Update(ClaimedCapabilityStandard claimedCapStd);
  }
#pragma warning restore CS1591
}
