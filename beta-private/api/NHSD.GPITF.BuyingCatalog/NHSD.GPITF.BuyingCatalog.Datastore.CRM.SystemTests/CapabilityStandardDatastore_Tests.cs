﻿using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NHSD.GPITF.BuyingCatalog.Interfaces;
using NHSD.GPITF.BuyingCatalog.Logic;
using NUnit.Framework;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Datastore.CRM.SystemTests
{
  [TestFixture]
  public sealed class CapabilityStandardDatastore_Tests : DatastoreBase_Tests<CapabilityStandardDatastore>
  {
    [Test]
    public void Constructor_Completes()
    {
      Assert.DoesNotThrow(() => new CapabilityStandardDatastore(DatastoreBaseSetup.CapabilityStandardDatastore, _logger, _policy, _config, new Mock<ILongTermCache>().Object));
    }

    [Test]
    public void GetAll_ReturnsData()
    {
      var datastore = new CapabilityStandardDatastore(DatastoreBaseSetup.CapabilityStandardDatastore, new Mock<ILogger<CapabilityStandardDatastore>>().Object, _policy, _config, new Mock<ILongTermCache>().Object);

      var datas = datastore.GetAll().ToList();

      datas.Should().NotBeEmpty();
      datas.ForEach(data => data.Should().NotBeNull());
      datas.ForEach(data => Verifier.Verify(data));
    }
  }
}
