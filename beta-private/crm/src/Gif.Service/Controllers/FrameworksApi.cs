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
  public class FrameworksApiController : Controller
  {
    /// <summary>
    /// Get existing framework/s which have the given capability
    /// </summary>

    private readonly IFrameworksDatastore _datastore;

    public FrameworksApiController(IFrameworksDatastore datastore)
    {
      _datastore = datastore;
    }

  /// <param name="capabilityId">CRM identifier of Capability</param>
  /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
  /// <param name="pageSize">number of items per page.  Defaults to 20</param>
  /// <response code="200">Success</response>
  /// <response code="404">Capability not found in CRM</response>
  [HttpGet]
    [Route("/api/Frameworks/ByCapability/{capabilityId}")]
    [ValidateModelState]
    [SwaggerOperation("ApiFrameworksByCapabilityByCapabilityIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListFrameworks), description: "Success")]
    public virtual IActionResult ApiFrameworksByCapabilityByCapabilityIdGet([FromRoute][Required]string capabilityId, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<Framework> frameworks;
      int totalPages;

      try
      {
        frameworks = _datastore.ByCapability(capabilityId);
        frameworks = frameworks.GetPagingValues(pageIndex, pageSize, out totalPages);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListFrameworks()
      {
        Items = frameworks.ToList(),
        PageSize = pageSize ?? Paging.DefaultPageSize,
        TotalPages = totalPages,
        PageIndex = pageIndex ?? Paging.DefaultIndex,
      });
    }

    /// <summary>
    /// Get an existing framework given its CRM identifier  Typically used to retrieve previous version
    /// </summary>

    /// <param name="id">CRM identifier of framework to find</param>
    /// <response code="200">Success</response>
    /// <response code="404">Framework not found in CRM</response>
    [HttpGet]
    [Route("/api/Frameworks/ById/{id}")]
    [ValidateModelState]
    [SwaggerOperation("ApiFrameworksByIdByIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(Framework), description: "Success")]
    public virtual IActionResult ApiFrameworksByIdByIdGet([FromRoute][Required]string id)
    {
      try
      {
        var framework = _datastore.ById(id);

        if (framework == null || framework?.Id == Guid.Empty)
          return StatusCode(404);

        return new ObjectResult(framework);

      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }
    }

    /// <summary>
    /// Get existing framework/s on which a solution was onboarded, given the CRM identifier of the solution
    /// </summary>

    /// <param name="solutionId">CRM identifier of solution</param>
    /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
    /// <param name="pageSize">number of items per page.  Defaults to 20</param>
    /// <response code="200">Success</response>
    /// <response code="404">Solution not found in CRM</response>
    [HttpGet]
    [Route("/api/Frameworks/BySolution/{solutionId}")]
    [ValidateModelState]
    [SwaggerOperation("ApiFrameworksBySolutionBySolutionIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListFrameworks), description: "Success")]
    public virtual IActionResult ApiFrameworksBySolutionBySolutionIdGet([FromRoute][Required]string solutionId, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<Framework> frameworks;
      int totalPages;

      try
      {
        frameworks = _datastore.BySolution(solutionId);
        frameworks = frameworks.GetPagingValues(pageIndex, pageSize, out totalPages);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListFrameworks()
      {
        Items = frameworks.ToList(),
        TotalPages = totalPages,
        PageSize = pageSize ?? Paging.DefaultPageSize,
        PageIndex = pageIndex ?? Paging.DefaultIndex
      });
    }

    /// <summary>
    /// Get existing framework/s which have the given standard
    /// </summary>

    /// <param name="standardId">CRM identifier of Standard</param>
    /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
    /// <param name="pageSize">number of items per page.  Defaults to 20</param>
    /// <response code="200">Success</response>
    /// <response code="404">Standard not found in CRM</response>
    [HttpGet]
    [Route("/api/Frameworks/ByStandard/{standardId}")]
    [ValidateModelState]
    [SwaggerOperation("ApiFrameworksByStandardByStandardIdGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListFrameworks), description: "Success")]
    public virtual IActionResult ApiFrameworksByStandardByStandardIdGet([FromRoute][Required]string standardId, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<Framework> frameworks;
      int totalPages;

      try
      {
        frameworks = _datastore.ByStandard(standardId);
        frameworks = frameworks.GetPagingValues(pageIndex, pageSize, out totalPages);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListFrameworks()
      {
        Items = frameworks.ToList(),
        TotalPages = totalPages,
        PageIndex = pageIndex ?? Paging.DefaultIndex,
        PageSize = pageSize ?? Paging.DefaultPageSize,
      });
    }

    /// <summary>
    /// Retrieve all current frameworks in a paged list
    /// </summary>

    /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
    /// <param name="pageSize">number of items per page.  Defaults to 20</param>
    /// <response code="200">Success - if no frameworks found, return empty list</response>
    [HttpGet]
    [Route("/api/Frameworks")]
    [ValidateModelState]
    [SwaggerOperation("ApiFrameworksGet")]
    [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListFrameworks), description: "Success - if no frameworks found, return empty list")]
    public virtual IActionResult ApiFrameworksGet([FromQuery]int? pageIndex, [FromQuery]int? pageSize)
    {
      IEnumerable<Framework> frameworks;
      int totalPages;

      try
      {
        frameworks = _datastore.GetAll();
        frameworks = frameworks.GetPagingValues(pageIndex, pageSize, out totalPages);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }

      return new ObjectResult(new PaginatedListFrameworks()
      {
        Items = frameworks.ToList(),
        TotalPages = totalPages,
        PageIndex = pageIndex ?? Paging.DefaultIndex,
        PageSize = pageSize ?? Paging.DefaultPageSize,
      });

    }
  }
}
