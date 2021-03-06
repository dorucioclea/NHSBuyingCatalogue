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
  /// 
  [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme + ",Bearer")]
  public class CapabilitiesApiController : Controller
  {
    /// <summary>
    /// Get existing Capability/s which are in the given Framework
    /// </summary>

    private readonly ICapabilityDatastore _datastore;

    public CapabilitiesApiController(ICapabilityDatastore datastore)
    {
      _datastore = datastore;
    }

    /// <param name="frameworkId">CRM identifier of Framework</param>
    /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
    /// <param name="pageSize">number of items per page.  Defaults to 20</param>
    /// <response code="200">Success</response>
    /// <response code="404">Framework not found in CRM</response>
    [HttpGet]
    [Route("/api/Capabilities/ByFramework/{frameworkId}")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesByFrameworkByFrameworkIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListCapabilities), description: "Success")]
    public virtual IActionResult ApiCapabilitiesByFrameworkByFrameworkIdGet([FromRoute][Required]string frameworkId, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<Capability> capabilities;
      int totalPages;

      try
      {
        capabilities = _datastore.ByFramework(frameworkId);
        capabilities = capabilities.GetPagingValues(pageIndex, pageSize, out totalPages);

        if (capabilities == null)
          return StatusCode(404);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListCapabilities()
      {
        Items = capabilities.ToList(),
        PageSize = pageSize ?? Paging.DefaultPageSize,
        TotalPages = totalPages,
        PageIndex = pageIndex ?? Paging.DefaultIndex
      });
    }

    /// <summary>
    /// Get an existing capability given its CRM identifier  Typically used to retrieve previous version
    /// </summary>

    /// <param name="id">CRM identifier of capability to find</param>
    /// <response code="200">Success</response>
    /// <response code="404">No capabilities not found in CRM</response>
    [HttpGet]
    [Route("/api/Capabilities/ById/{id}")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesByIdByIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Capability), description: "Success")]
    public virtual IActionResult ApiCapabilitiesByIdByIdGet([FromRoute][Required]string id)
    {
      try
      {
        var capability = _datastore.ById(id);

        if (capability == null || capability?.Id == Guid.Empty)
          return StatusCode(404);

        return new ObjectResult(capability);

      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }
    }

    /// <summary>
    /// Get several existing capabilities given their CRM identifiers
    /// </summary>

    /// <param name="ids">Array of CRM identifiers of capabilities to find</param>
    /// <response code="200">Success</response>
    /// <response code="404">Capabilities not found in CRM</response>
    [HttpPost]
    [Route("/api/Capabilities/ByIds")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesByIdsPost")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<Capability>), description: "Success")]
    public virtual IActionResult ApiCapabilitiesByIdsPost([FromBody]List<string> ids)
    {
      try
      {
        var capabilities = _datastore.ByIds(ids);

        if (capabilities == null)
          return StatusCode(404);

        return new ObjectResult(capabilities);

      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

    }

    /// <summary>
    /// Get existing Capability/s which require the given/optional Standard
    /// </summary>

    /// <param name="standardId">CRM identifier of Standard</param>
    /// <param name="isOptional">true if the specified Standard is optional with the Capability</param>
    /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
    /// <param name="pageSize">number of items per page.  Defaults to 20</param>
    /// <response code="200">Success</response>
    /// <response code="404">Capability not found in CRM</response>
    [HttpGet]
    [Route("/api/Capabilities/ByStandard/{standardId}")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesByStandardByStandardIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListCapabilities), description: "Success")]
    public virtual IActionResult ApiCapabilitiesByStandardByStandardIdGet([FromRoute][Required]string standardId, [FromQuery]bool? isOptional, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<Capability> capabilities;
      int totalPages;

      try
      {
        capabilities = _datastore.ByStandard(standardId, isOptional != null && isOptional.Value);
        capabilities = capabilities.GetPagingValues(pageIndex, pageSize, out totalPages);

        if (capabilities == null)
          return StatusCode(404);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListCapabilities()
      {
        Items = capabilities.ToList(),
        PageSize = pageSize ?? Paging.DefaultPageSize,
        TotalPages = totalPages,
        PageIndex = pageIndex ?? Paging.DefaultIndex
      });

    }

    /// <summary>
    /// Retrieve all current capabilities in a paged list
    /// </summary>

    /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
    /// <param name="pageSize">number of items per page.  Defaults to 20</param>
    /// <response code="200">Success - if no capabilities found, return empty list</response>
    [HttpGet]
    [Route("/api/Capabilities")]
    [ValidateModelState]
    [SwaggerOperation("ApiCapabilitiesGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListCapabilities), description: "Success - if no capabilities found, return empty list")]
    public virtual IActionResult ApiCapabilitiesGet([FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<Capability> capabilities;
      int totalPages;

      try
      {
        capabilities = _datastore.GetAll();
        capabilities = capabilities.GetPagingValues(pageIndex, pageSize, out totalPages);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListCapabilities()
      {
        Items = capabilities.ToList(),
        PageSize = pageSize ?? Paging.DefaultPageSize,
        TotalPages = totalPages,
        PageIndex = pageIndex ?? Paging.DefaultIndex
      });
    }
  }
}
