﻿using Dapper;
using Microsoft.Extensions.Logging;
using NHSD.GPITF.BuyingCatalog.Datastore.Database.Interfaces;
using NHSD.GPITF.BuyingCatalog.Interfaces;
using NHSD.GPITF.BuyingCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Datastore.Database
{
  public sealed class KeywordSearchHistoryDatastore : DatastoreBase<KeywordSearchHistory>, IKeywordSearchHistoryDatastore
  {
    public KeywordSearchHistoryDatastore(
      IDbConnectionFactory dbConnectionFactory,
      ILogger<DatastoreBase<KeywordSearchHistory>> logger,
      ISyncPolicyFactory policy) :
      base(dbConnectionFactory, logger, policy)
    {
    }

    public IEnumerable<KeywordSearchHistory> Get(DateTime startDate, DateTime endDate)
    {
      return GetInternal(() =>
      {
        const string SearchByKeywordCallsite = "NHSD.GPITF.BuyingCatalog.Search.Porcelain.SearchDatastore.ByKeyword";
        var sql = $@"
select log.Timestamp, log.Message as Keyword from Log log
where Callsite like '{SearchByKeywordCallsite}'
";
        // cannot use GetAll() because this requires an explicit key
        var retval = _dbConnection.Value.Query<KeywordSearchHistory>(sql)
          .Where(x => x.Timestamp >= startDate && x.Timestamp <= endDate);
        return retval;
      });
    }
  }
}
