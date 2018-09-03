﻿using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Moq;
using NHSD.GPITF.BuyingCatalog.Interfaces;
using NHSD.GPITF.BuyingCatalog.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Logic.Tests
{
  [TestFixture]
  public sealed class CapabilitiesImplementedValidator_Tests
  {
    private Mock<IHttpContextAccessor> _context;
    private Mock<ICapabilitiesImplementedDatastore> _claimDatastore;
    private Mock<ISolutionsDatastore> _solutionsDatastore;

    [SetUp]
    public void SetUp()
    {
      _context = new Mock<IHttpContextAccessor>();
      _claimDatastore = new Mock<ICapabilitiesImplementedDatastore>();
      _solutionsDatastore = new Mock<ISolutionsDatastore>();
    }

    [Test]
    public void Constructor_Completes()
    {
      Assert.DoesNotThrow(() => new CapabilitiesImplementedValidator(_context.Object, _claimDatastore.Object, _solutionsDatastore.Object));
    }

    [TestCase(Roles.Supplier)]
    public void Validate_Delete_ValidRole_Draft_Succeeds(string role)
    {
      var orgId = Guid.NewGuid().ToString();
      _context.Setup(x => x.HttpContext).Returns(Creator.GetContext(role: role, orgId: orgId));
      var validator = new CapabilitiesImplementedValidator(_context.Object, _claimDatastore.Object, _solutionsDatastore.Object);
      var claim = GetCapabilitiesImplemented(status: CapabilitiesImplementedStatus.Draft);
      _claimDatastore.Setup(x => x.ById(claim.Id)).Returns(claim);
      _solutionsDatastore.Setup(x => x.ById(claim.SolutionId)).Returns(Creator.GetSolution(orgId: orgId));

      var valres = validator.Validate(claim, ruleSet: nameof(ICapabilitiesImplementedLogic.Delete));

      valres.Errors.Should().BeEmpty();
    }

    [TestCase(Roles.Buyer)]
    [TestCase(Roles.Admin)]
    public void Validate_Delete_InvalidRole_Draft_ReturnsError(string role)
    {
      var orgId = Guid.NewGuid().ToString();
      _context.Setup(x => x.HttpContext).Returns(Creator.GetContext(role: role, orgId: orgId));
      var validator = new CapabilitiesImplementedValidator(_context.Object, _claimDatastore.Object, _solutionsDatastore.Object);
      var claim = GetCapabilitiesImplemented(status: CapabilitiesImplementedStatus.Draft);
      _claimDatastore.Setup(x => x.ById(claim.Id)).Returns(claim);
      _solutionsDatastore.Setup(x => x.ById(claim.SolutionId)).Returns(Creator.GetSolution(orgId: orgId));

      var valres = validator.Validate(claim, ruleSet: nameof(ICapabilitiesImplementedLogic.Delete));

      valres.Errors.Count().Should().Be(1);
    }

    [Test]
    public void Validate_Delete_NonDraft_ReturnsError(
      [Values(
        Roles.Buyer,
        Roles.Supplier,
        Roles.Admin
      )]
        string role,
      [Values(
        CapabilitiesImplementedStatus.Submitted,
        CapabilitiesImplementedStatus.Remediation,
        CapabilitiesImplementedStatus.Approved,
        CapabilitiesImplementedStatus.Rejected
        )]
        CapabilitiesImplementedStatus status)
    {
      var orgId = Guid.NewGuid().ToString();
      _context.Setup(x => x.HttpContext).Returns(Creator.GetContext(role: role, orgId: orgId));
      var validator = new CapabilitiesImplementedValidator(_context.Object, _claimDatastore.Object, _solutionsDatastore.Object);
      var claim = GetCapabilitiesImplemented(status: status);
      _claimDatastore.Setup(x => x.ById(claim.Id)).Returns(claim);
      _solutionsDatastore.Setup(x => x.ById(claim.SolutionId)).Returns(Creator.GetSolution(orgId: orgId));

      var valres = validator.Validate(claim, ruleSet: nameof(ICapabilitiesImplementedLogic.Delete));

      valres.Errors.Count().Should().Be(1);
    }

    private static CapabilitiesImplemented GetCapabilitiesImplemented(
      string id = null,
      string solnId = null,
      string claimId = null,
      CapabilitiesImplementedStatus status = CapabilitiesImplementedStatus.Draft)
    {
      return new CapabilitiesImplemented
      {
        Id = id ?? Guid.NewGuid().ToString(),
        SolutionId = solnId ?? Guid.NewGuid().ToString(),
        CapabilityId = claimId ?? Guid.NewGuid().ToString(),
        Status = status
      };
    }
  }
}
