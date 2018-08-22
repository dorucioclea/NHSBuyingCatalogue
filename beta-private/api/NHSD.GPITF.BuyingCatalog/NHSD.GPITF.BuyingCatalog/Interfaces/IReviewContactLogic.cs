﻿using NHSD.GPITF.BuyingCatalog.Models;
using System.Linq;

namespace NHSD.GPITF.BuyingCatalog.Interfaces
{
#pragma warning disable CS1591
  public interface IReviewContactLogic
  {
    IQueryable<ReviewContact> ByAssessmentMessage(string assMessId);
    ReviewContact Create(ReviewContact assMessCont);
    void Update(ReviewContact assMessCont);
    void Delete(ReviewContact assMessCont);
  }
#pragma warning restore CS1591
}