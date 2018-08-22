﻿using NHSD.GPITF.BuyingCatalog.Models;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Interfaces
{
#pragma warning disable CS1591
  public interface ICapabilitiesImplementedEvidenceDatastore
  {
    IQueryable<CapabilitiesImplementedEvidence> ByCapabilitiesImplemented(string capabilitiesImplementedId);
    CapabilitiesImplementedEvidence Create(CapabilitiesImplementedEvidence capEvidenc);
  }
#pragma warning restore CS1591
}
