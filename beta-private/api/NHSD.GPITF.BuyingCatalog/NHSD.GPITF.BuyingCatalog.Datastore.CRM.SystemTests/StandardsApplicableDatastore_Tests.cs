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
  public sealed class StandardsApplicableDatastore_Tests : DatastoreBase_Tests<StandardsApplicableDatastore>
  {
    [Test]
    public void Constructor_Completes()
    {
      Assert.DoesNotThrow(() => new StandardsApplicableDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy));
    }

    [Test]
    public void BySolution_ReturnsData()
    {
      var allSolns = Retriever.GetAllSolutions(_policy);
      var ids = allSolns.Select(soln => soln.Id).Distinct().ToList();
      var datastore = new StandardsApplicableDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);

      var datas = ids.SelectMany(id => datastore.BySolution(id)).ToList();

      datas.Should().NotBeEmpty();
      datas.ForEach(data => Verifier.Verify(data));
    }

    [Test]
    public void CRUD_Succeeds()
    {
      var soln = Retriever.GetAllSolutions(_policy).First();
      var std = Retriever.GetAllStandards(_policy).First();
      var datastore = new StandardsApplicableDatastore(DatastoreBaseSetup.CrmConnectionFactory, _logger, _policy);

      // create
      var newEnt = new StandardsApplicable
      {
        Id = Guid.NewGuid().ToString(),
        SolutionId = soln.Id,
        StandardId = std.Id,
        Status = StandardsApplicableStatus.Draft
      };
      Verifier.Verify(newEnt);
      var createdEnt = datastore.Create(newEnt);

      try
      {
        createdEnt.Should().BeEquivalentTo(newEnt);

        // update
        createdEnt.Status = StandardsApplicableStatus.Submitted;
        datastore.Update(createdEnt);

        // retrieve
        datastore.ById(createdEnt.Id)
          .Should().BeEquivalentTo(createdEnt);
      }
      finally
      {
        // delete
        datastore.Delete(createdEnt);
      }

      // delete
      datastore.ById(createdEnt.Id)
        .Should().BeNull();
    }
  }
}
