﻿using NHSD.GPITF.BuyingCatalog.Models;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Interfaces
{
#pragma warning disable CS1591
  public interface IFrameworksDatastore
  {
    IQueryable<Frameworks> ByCapability(string capabilityId);
    Frameworks ById(string id);
    IQueryable<Frameworks> BySolution(string solutionId);
    IQueryable<Frameworks> ByStandard(string standardId);
    IQueryable<Frameworks> GetAll();
  }
#pragma warning restore CS1591
}