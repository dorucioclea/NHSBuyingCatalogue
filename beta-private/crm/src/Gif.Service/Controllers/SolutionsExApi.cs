/*
 * catalogue-api
 *
 * NHS Digital GP IT Futures Buying Catalog API
 *
 * OpenAPI spec version: 1.0.0-private-beta
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Gif.Service.Attributes;
using Gif.Service.Contracts;
using Gif.Service.Crm;
using Gif.Service.Models;
using Gif.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using ZNetCS.AspNetCore.Authentication.Basic;

namespace Gif.Service.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [Authorize(AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme + ",Bearer")]
  public class SolutionsExApiController : Controller
  {
    /// <summary>
    /// Update an existing Solution, TechnicalContact, ClaimedCapability, ClaimedStandard et al with new information
    /// </summary>

    private readonly ISolutionsExDatastore _datastore;
    private readonly IFrameworksDatastore _frameworksDatastore;
    private readonly ILinkManagerDatastore _linkManagerDatastore;

    public SolutionsExApiController(
      ISolutionsExDatastore datastore,
      IFrameworksDatastore frameworksDatastore,
      ILinkManagerDatastore linkManagerDatastore)
    {
      _datastore = datastore;
      _frameworksDatastore = frameworksDatastore;
      _linkManagerDatastore = linkManagerDatastore;
    }

  /// <param name="solnEx">Solution, TechnicalContact, ClaimedCapability, ClaimedStandard et al with updated information</param>
  /// <response code="200">Success</response>
  /// <response code="404">Solution, TechnicalContact, ClaimedCapability, ClaimedStandard et al not found in CRM</response>
  /// <response code="500">Datastore exception</response>
  [HttpPut]
    [Route("/api/porcelain/SolutionsEx/Update")]
    [ValidateModelState]
    [SwaggerOperation("ApiPorcelainSolutionsExUpdatePut")]
    public virtual IActionResult ApiPorcelainSolutionsExUpdatePut([FromBody]SolutionEx solnEx)
    {
      var solutionFrameworks = new List<Framework>();

      try
      {
        solutionFrameworks = _frameworksDatastore.BySolution(solnEx.Solution.Id.ToString()).ToList();
        _datastore.Update(solnEx);
      }
      catch (Crm.CrmApiException ex)
      {
        return StatusCode((int)ex.HttpStatus, ex.Message);
      }
      finally
      {

        foreach (var solutionFramework in solutionFrameworks)
        {
          _linkManagerDatastore.FrameworkSolutionAssociate(solutionFramework.Id, solnEx.Solution.Id);
        }
      }

      return StatusCode(200, solnEx);
    }
  }
}
