﻿using Microsoft.AspNetCore.Http;
using NHSD.GPITF.BuyingCatalog.Interfaces;
using NHSD.GPITF.BuyingCatalog.Models;

namespace NHSD.GPITF.BuyingCatalog.Logic.Tests
{
  public sealed class DummyEvidenceValidatorBase : EvidenceValidatorBase<EvidenceBase>
  {
    public DummyEvidenceValidatorBase(
      IStandardsApplicableDatastore claimDatastore,
      ISolutionsDatastore solutionDatastore,
      IHttpContextAccessor context) :
      base((IClaimsDatastore<ClaimsBase>)claimDatastore, solutionDatastore, context)
    {
    }

    protected override SolutionStatus SolutionReviewStatus => SolutionStatus.Failed;
  }
}
