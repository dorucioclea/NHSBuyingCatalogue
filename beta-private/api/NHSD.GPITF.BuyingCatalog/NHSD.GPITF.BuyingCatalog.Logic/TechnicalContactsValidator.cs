﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using NHSD.GPITF.BuyingCatalog.Interfaces;
using NHSD.GPITF.BuyingCatalog.Models;

namespace NHSD.GPITF.BuyingCatalog.Logic
{
  public sealed class TechnicalContactsValidator : ValidatorBase<TechnicalContacts>, ITechnicalContactsValidator
  {
    private readonly ISolutionsDatastore _solutionDatastore;

    public TechnicalContactsValidator(
      IHttpContextAccessor context,
      ISolutionsDatastore solutionDatastore) :
      base(context)
    {
      _solutionDatastore = solutionDatastore;

      RuleSet(nameof(ITechnicalContactsLogic.Create), () =>
      {
        MustBeAdminOrSupplier();
        SupplierOwn();
      });
      RuleSet(nameof(ITechnicalContactsLogic.Update), () =>
      {
        MustBeAdminOrSupplier();
        SupplierOwn();
      });
      RuleSet(nameof(ITechnicalContactsLogic.Delete), () =>
      {
        MustBeAdminOrSupplier();
        SupplierOwn();
      });
    }

    private void SupplierOwn()
    {
      RuleFor(x => x)
        .Must(x => 
        {
          if (_context.HasRole(Roles.Supplier))
          {
            var soln = _solutionDatastore.ById(x.SolutionId);
            return _context.OrganisationId() == soln.OrganisationId;
          }
          return _context.HasRole(Roles.Admin);
        })
        .WithMessage("Supplier can only change own Technical Contacts");
    }
  }
}
