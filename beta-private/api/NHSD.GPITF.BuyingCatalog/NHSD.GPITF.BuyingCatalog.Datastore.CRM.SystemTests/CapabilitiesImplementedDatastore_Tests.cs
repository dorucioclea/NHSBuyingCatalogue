﻿using FluentAssertions;
using NHSD.GPITF.BuyingCatalog.Logic;
using NHSD.GPITF.BuyingCatalog.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Datastore.CRM.SystemTests
{
  [TestFixture]
  public sealed class CapabilitiesImplementedDatastore_Tests : ClaimsDatastoreBase_Tests<CapabilitiesImplementedDatastore>
  {
    [Test]
    public void Constructor_Completes()
    {
      Assert.DoesNotThrow(() => new CapabilitiesImplementedDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy));
    }

    [Test]
    public void BySolution_ReturnsData()
    {
      var allSolns = GetAllSolutions();
      var ids = allSolns.Select(soln => soln.Id).Distinct();
      var datastore = new CapabilitiesImplementedDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);

      var datas = ids.ToList().SelectMany(id => datastore.BySolution(id));

      datas.Should().NotBeEmpty();
      datas.ToList().ForEach(data => Verifier.Verify(data));
    }

    [Test]
    public void CRUD_Succeeds()
    {
      var soln = GetAllSolutions().First();
      var cap = GetAllCapabilities().First();
      var datastore = new CapabilitiesImplementedDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);

      // create
      var newEnt = new CapabilitiesImplemented
      {
        Id = Guid.NewGuid().ToString(),
        SolutionId = soln.Id,
        CapabilityId = cap.Id,
        Status = CapabilitiesImplementedStatus.Draft
      };
      Verifier.Verify(newEnt);
      var createdEnt = datastore.Create(newEnt);
      createdEnt.Should().BeEquivalentTo(newEnt, opt => opt.Excluding(ent => ent.Id));

      try
      {
        // update
        createdEnt.Status = CapabilitiesImplementedStatus.Submitted;
        datastore.Update(createdEnt);

        // retrieve
        var updatedEnt = datastore.ById(createdEnt.Id);
        updatedEnt.Should().BeEquivalentTo(createdEnt);
      }
      finally
      {
        // delete
        datastore.Delete(createdEnt);
      }

      // delete
      var deletedEnt = datastore.ById(createdEnt.Id);
      deletedEnt.Should().BeNull();
    }
  }
}
