﻿using Microsoft.Extensions.Configuration;
using NHSD.GPITF.BuyingCatalog.Datastore.Database.Interfaces;
using System;
using System.Data;

namespace NHSD.GPITF.BuyingCatalog.Datastore.Database
{
  public sealed class DbConnectionFactory : IDbConnectionFactory
  {
    private readonly IConfiguration _config;

    public DbConnectionFactory(IConfiguration config)
    {
      _config = config;
    }

    public IDbConnection Get()
    {
      var dbConfig = _config.GetSection("RepositoryDatabase");
      var connNode = dbConfig["Connection"];
      var connConfig = dbConfig.GetSection(connNode);
      var dbType = Enum.Parse<DataAccessProviderTypes>(connConfig["Type"]);
      var fact = DbProviderFactoryUtils.GetDbProviderFactory(dbType);
      var conn = fact.CreateConnection();

      conn.ConnectionString = connConfig["ConnectionString"].Replace("|DataDirectory|", AppDomain.CurrentDomain.BaseDirectory);
      conn.Open();

      return conn;
    }
  }
}
