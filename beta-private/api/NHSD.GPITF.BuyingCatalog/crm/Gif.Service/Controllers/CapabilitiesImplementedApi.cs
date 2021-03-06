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
  public class CapabilitiesImplementedApiController : Controller
  {
    /// <summary>
    /// Retrieve claim, given the claim’s CRM identifier
    /// </summary>

    private readonly ICapabilitiesImplementedDatastore _datastore;

    public CapabilitiesImplementedApiController(ICapabilitiesImplementedDatastore datastore)
    {
      _datastore = datastore;
    }

    /// <param name="id">CRM identifier of claim</param>
    /// <response code="200">Success</response>
    /// <response code="404">Claim not found in CRM</response>
    [HttpGet]
    [Route("/api/CapabilitiesImplemented/ById/{id}")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesImplementedByIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(CapabilityImplemented), description: "Success")]
    public virtual IActionResult ApiCapabilitiesImplementedByIdGet([FromRoute][Required]string id)
    {
      try
      {
        var capabilityImplemented = _datastore.ById(id);

        if (capabilityImplemented == null || capabilityImplemented?.Id == Guid.Empty)
          return StatusCode(404);

        return new ObjectResult(capabilityImplemented);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }
    }

    /// <summary>
    /// Retrieve all claimed capabilities for a solution in a paged list,  given the solution’s CRM identifier
    /// </summary>

    /// <param name="solutionId">CRM identifier of solution</param>
    /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
    /// <param name="pageSize">number of items per page.  Defaults to 20</param>
    /// <response code="200">Success</response>
    /// <response code="404">Solution not found in CRM</response>
    [HttpGet]
    [Route("/api/CapabilitiesImplemented/BySolution/{solutionId}")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesImplementedBySolutionBySolutionIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListCapabilitiesImplemented), description: "Success")]
    public virtual IActionResult ApiCapabilitiesImplementedBySolutionBySolutionIdGet([FromRoute][Required]string solutionId, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<CapabilityImplemented> capabilitiesImplemented;
      int totalPages;

      try
      {
        capabilitiesImplemented = _datastore.BySolution(solutionId);
        capabilitiesImplemented = capabilitiesImplemented.GetPagingValues(pageIndex, pageSize, out totalPages);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }


      return new ObjectResult(new PaginatedListCapabilitiesImplemented()
      {
        Items = capabilitiesImplemented.ToList(),
        PageSize = pageSize ?? Paging.DefaultPageSize,
        PageIndex = pageIndex ?? Paging.DefaultIndex,
        TotalPages = totalPages
      });
    }

    /// <summary>
    /// Delete an existing claimed capability for a solution
    /// </summary>

    /// <param name="claimedcapability">existing claimed capability information</param>
    /// <response code="200">Success</response>
    /// <response code="404">ClaimedCapability not found in CRM</response>
    [HttpDelete]
    [Route("/api/CapabilitiesImplemented")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesImplementedDelete")]
    public virtual IActionResult ApiCapabilitiesImplementedDelete([FromBody]CapabilityImplemented claimedcapability)
    {
      try
      {
        var capabilityImplemented = _datastore.ById(claimedcapability.Id.ToString());

        if (capabilityImplemented == null || capabilityImplemented?.Id == Guid.Empty)
          return StatusCode(404);

        _datastore.Delete(claimedcapability);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return StatusCode(200);
    }

    /// <summary>
    /// Create a new claimed capability for a solution
    /// </summary>

    /// <param name="claimedcapability">new claimed capability information</param>
    /// <response code="200">Success</response>
    /// <response code="404">Solution not found in CRM</response>
    [HttpPost]
    [Route("/api/CapabilitiesImplemented")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesImplementedPost")]
    [SwaggerResponse(statusCode: 200, type: typeof(CapabilityImplemented), description: "Success")]
    public virtual IActionResult ApiCapabilitiesImplementedPost([FromBody]CapabilityImplemented claimedcapability)
    {
      CapabilityImplemented capabilityImplemented;

      try
      {
        capabilityImplemented = _datastore.Create(claimedcapability);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(capabilityImplemented);
    }

    /// <summary>
    /// Update an existing claimed capability with new information
    /// </summary>

    /// <param name="claimedcapability">claimed capability with updated information</param>
    /// <response code="200">Success</response>
    /// <response code="404">Solution or ClaimedCapability not found in CRM</response>
    [HttpPut]
    [Route("/api/CapabilitiesImplemented")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesImplementedPut")]
    public virtual IActionResult ApiCapabilitiesImplementedPut([FromBody]CapabilityImplemented claimedcapability)
    {
      try
      {
        _datastore.Update(claimedcapability);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return StatusCode(200);
    }
  }
}
