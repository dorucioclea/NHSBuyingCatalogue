﻿using FluentValidation;
using NHSD.GPITF.BuyingCatalog.Models;

namespace NHSD.GPITF.BuyingCatalog.Logic
{
  public interface ICapabilityValidator : IValidator<Capability>
  {
  }
}
