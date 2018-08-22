﻿using Microsoft.AspNetCore.Http;
using NHSD.GPITF.BuyingCatalog.Models;

namespace NHSD.GPITF.BuyingCatalog.Logic
{
  public sealed class SolutionsFilter : FilterBase<Solutions>, ISolutionsFilter
  {
    public SolutionsFilter(IHttpContextAccessor context) :
      base(context)
    {
    }

    protected override Solutions Filter(Solutions input)
    {
      if (_context.HasRole(Roles.Admin))
      {
        return input;
      }

      if (_context.HasRole(Roles.Buyer))
      {
        // Buyer: hide draft Solutions
        return input.Status == SolutionStatus.Draft ? null : input;
      }

      if (_context.HasRole(Roles.Supplier))
      {
        // Supplier: only own Solutions
        return _context.OrganisationId() == input.OrganisationId ? input : null;
      }

      // None: hide draft Solutions
      return input.Status == SolutionStatus.Draft ? null : input;
    }
  }
}