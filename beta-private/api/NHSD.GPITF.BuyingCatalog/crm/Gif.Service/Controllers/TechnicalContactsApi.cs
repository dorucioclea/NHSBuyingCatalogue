using Gif.Service.Attributes;
using Gif.Service.Const;
using Gif.Service.Contracts;
using Gif.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZNetCS.AspNetCore.Authentication.Basic;

namespace Gif.Service.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme + ",Bearer")]
  public class TechnicalContactsApiController : Controller
  {
    /// <summary>
    /// Retrieve all Technical Contacts for a solution in a paged list,  given the solution’s CRM identifier
    /// </summary>

    private readonly ITechnicalContactsDatastore _datastore;

    public TechnicalContactsApiController(ITechnicalContactsDatastore datastore)
    {
      _datastore = datastore;
    }

  /// <param name="solutionId">CRM identifier of solution</param>
  /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
  /// <param name="pageSize">number of items per page.  Defaults to 20</param>
  /// <response code="200">Success</response>
  /// <response code="404">Solution not found in CRM</response>
  [HttpGet]
    [Route("/api/TechnicalContacts/BySolution/{solutionId}")]
    [ValidateModelState]
    [SwaggerOperation("ApiTechnicalContactsBySolutionBySolutionIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListTechnicalContacts), description: "Success")]
    public virtual IActionResult ApiTechnicalContactsBySolutionBySolutionIdGet([FromRoute][Required]string solutionId, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<TechnicalContact> technicalContacts;
      int totalPages;

      try
      {
        technicalContacts = _datastore.BySolution(solutionId);
        technicalContacts = technicalContacts.GetPagingValues(pageIndex, pageSize, out totalPages);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListTechnicalContacts
      {
        Items = technicalContacts.ToList(),
        TotalPages = totalPages,
        PageIndex = pageIndex ?? Paging.DefaultIndex,
        PageSize = pageSize ?? Paging.DefaultPageSize,
      });

    }

    /// <summary>
    /// Delete an existing Technical Contact for a Solution
    /// </summary>

    /// <param name="techCont">existing Technical Contact information</param>
    /// <response code="200">Success</response>
    /// <response code="404">Technical Contact or Solution not found in CRM</response>
    [HttpDelete]
    [Route("/api/TechnicalContacts")]
    [ValidateModelState]
    [SwaggerOperation("ApiTechnicalContactsDelete")]
    public virtual IActionResult ApiTechnicalContactsDelete([FromBody]TechnicalContact techCont)
    {
      try
      {
        var techContGet = _datastore.ById(techCont.Id.ToString());

        if (techContGet == null || techContGet?.Id == Guid.Empty)
          return StatusCode(404);

        _datastore.Delete(techCont);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return StatusCode(200);
    }

    /// <summary>
    /// Create a new Technical Contact for a Solution
    /// </summary>

    /// <param name="techCont">new Technical Contact information</param>
    /// <response code="200">Success</response>
    /// <response code="404">Solution not found in CRM</response>
    [HttpPost]
    [Route("/api/TechnicalContacts")]
    [ValidateModelState]
    [SwaggerOperation("ApiTechnicalContactsPost")]
    [SwaggerResponse(statusCode: 200, type: typeof(TechnicalContact), description: "Success")]
    public virtual IActionResult ApiTechnicalContactsPost([FromBody]TechnicalContact techCont)
    {
      try
      {
        _datastore.Create(techCont);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(techCont);
    }

    /// <summary>
    /// Update a Technical Contact with new information
    /// </summary>

    /// <param name="techCont">Technical Contact with updated information</param>
    /// <response code="200">Success</response>
    /// <response code="404">Technical Contact or Solution not found in CRM</response>
    [HttpPut]
    [Route("/api/TechnicalContacts")]
    [ValidateModelState]
    [SwaggerOperation("ApiTechnicalContactsPut")]
    public virtual IActionResult ApiTechnicalContactsPut([FromBody]TechnicalContact techCont)
    {

      try
      {
        _datastore.Update(techCont);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return StatusCode(200, techCont);
    }
  }
}
