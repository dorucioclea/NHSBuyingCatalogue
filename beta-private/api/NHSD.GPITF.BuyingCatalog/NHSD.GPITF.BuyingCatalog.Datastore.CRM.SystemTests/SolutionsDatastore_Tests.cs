﻿using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NHSD.GPITF.BuyingCatalog.Logic;
using NHSD.GPITF.BuyingCatalog.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Datastore.CRM.SystemTests
{
  [TestFixture]
  public sealed class SolutionsDatastore_Tests : DatastoreBase_Tests<SolutionsDatastore>
  {
    [Test]
    public void Constructor_Completes()
    {
      Assert.DoesNotThrow(() => new SolutionsDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy));
    }

    [Test]
    public void ByFramework_ReturnsData()
    {
      var otherDatastore = new FrameworksDatastore(DatastoreBaseSetup.CrmConnectionFactory, new Mock<ILogger<FrameworksDatastore>>().Object, _policy);
      var others = otherDatastore.GetAll();
      var datastore = new SolutionsDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);

      var datas = others.ToList().SelectMany(other => datastore.ByFramework(other.Id));

      datas.Should().NotBeEmpty();
      datas.ToList().ForEach(data => Verifier.Verify(data));
    }

    [Test]
    public void ById_UnknownId_ReturnsNull()
    {
      var datastore = new SolutionsDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);

      var data = datastore.ById(Guid.NewGuid().ToString());

      data.Should().BeNull();
    }

    [Test]
    public void ById_KnownId_ReturnsData()
    {
      var otherDatastore = new FrameworksDatastore(DatastoreBaseSetup.CrmConnectionFactory, new Mock<ILogger<FrameworksDatastore>>().Object, _policy);
      var others = otherDatastore.GetAll();
      var datastore = new SolutionsDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);
      var allData = others.ToList().SelectMany(other => datastore.ByFramework(other.Id));

      var allDataById = allData.Select(data => datastore.ById(data.Id));

      allDataById.Should().BeEquivalentTo(allData);
    }

    [Test]
    public void ByOrganisation_UnknownId_ReturnsEmpty()
    {
      var datastore = new SolutionsDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);

      var data = datastore.ByOrganisation(Guid.NewGuid().ToString());

      data.Should().BeEmpty();
    }

    [Test]
    public void ByOrganisation_KnownId_ReturnsData()
    {
      var otherDatastore = new FrameworksDatastore(DatastoreBaseSetup.CrmConnectionFactory, new Mock<ILogger<FrameworksDatastore>>().Object, _policy);
      var others = otherDatastore.GetAll();
      var datastore = new SolutionsDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);
      var orgIds = others.ToList()
        .SelectMany(other => datastore.ByFramework(other.Id))
        .Select(soln => soln.OrganisationId)
        .Distinct();

      var allDataByOrg = orgIds.SelectMany(orgId => datastore.ByOrganisation(orgId));

      allDataByOrg.Should().NotBeEmpty();
      allDataByOrg.ToList().ForEach(soln => Verifier.Verify(soln));
    }

    [Test]
    public void CRUD_Succeeds()
    {
      var frameworksDatastore = new FrameworksDatastore(DatastoreBaseSetup.CrmConnectionFactory, new Mock<ILogger<FrameworksDatastore>>().Object, _policy);
      var frameworks = frameworksDatastore.GetAll();
      var datastore = new SolutionsDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);
      var orgId = frameworks.ToList()
        .SelectMany(other => datastore.ByFramework(other.Id))
        .Select(soln => soln.OrganisationId)
        .First();
      var contactsDatastore = new ContactsDatastore(DatastoreBaseSetup.CrmConnectionFactory, new Mock<ILogger<ContactsDatastore>>().Object, _policy);
      var contactId = contactsDatastore.ByOrganisation(orgId).First().Id;

      var newSoln = new Solutions
      {
        Id = string.Empty,
        PreviousId = null,
        OrganisationId = orgId,
        Name = "My New Solution",
        CreatedOn = DateTime.UtcNow,
        ModifiedOn = DateTime.UtcNow,
        CreatedById = contactId,
        ModifiedById = contactId
      };
      var createdSoln = datastore.Create(newSoln);
      createdSoln.Should().BeEquivalentTo(newSoln, opt => opt.Excluding(soln => soln.Id));
      try
      {
        var retrievedSoln = datastore.ById(createdSoln.Id);
        retrievedSoln.Should().BeEquivalentTo(createdSoln);

        createdSoln.Name = "My Other New Solution";
        datastore.Update(createdSoln);
        var updatedSoln = datastore.ById(createdSoln.Id);
        updatedSoln.Should().BeEquivalentTo(createdSoln);
      }
      finally
      {
        datastore.Delete(createdSoln);
      }

      var deletedSoln = datastore.ById(createdSoln.Id);
      deletedSoln.Should().BeNull();
    }
  }
}
