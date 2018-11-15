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
using Gif.Service.Const;
using Gif.Service.Crm;
using Gif.Service.Models;
using Gif.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gif.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class StandardsApplicableApiController : Controller
    {
        /// <summary>
        /// Retrieve claim, given the claim’s CRM identifier
        /// </summary>

        /// <param name="id">CRM identifier of claim</param>
        /// <response code="200">Success</response>
        /// <response code="404">Claim not found in CRM</response>
        [HttpGet]
        [Route("/api/StandardsApplicable/{id}")]
        [ValidateModelState]
        [SwaggerOperation("ApiStandardsApplicableByIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(StandardApplicable), description: "Success")]
        public virtual IActionResult ApiStandardsApplicableByIdGet([FromRoute][Required]string id)
        {
            try
            {
                var standard = new StandardsApplicableService(new Repository()).ById(id);

                if (standard.Id == Guid.Empty)
                    return StatusCode(404);

                return new ObjectResult(standard);
            }
            catch (Crm.CrmApiException ex)
            {
                return StatusCode((int)ex.HttpStatus, ex.Message);
            }

        }

        /// <summary>
        /// Retrieve all claimed standards for a solution in a paged list,   given the solution’s CRM identifier
        /// </summary>

        /// <param name="solutionId">CRM identifier of solution</param>
        /// <param name="pageIndex">1-based index of page to return.  Defaults to 1</param>
        /// <param name="pageSize">number of items per page.  Defaults to 20</param>
        /// <response code="200">Success</response>
        /// <response code="404">Solution not found in CRM</response>
        [HttpGet]
        [Route("/api/StandardsApplicable/BySolution/{solutionId}")]
        [ValidateModelState]
        [SwaggerOperation("ApiStandardsApplicableBySolutionBySolutionIdGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaginatedListStandardsApplicable), description: "Success")]
        public virtual IActionResult ApiStandardsApplicableBySolutionBySolutionIdGet([FromRoute][Required]string solutionId, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        {
            IEnumerable<StandardApplicable> standardsApplicable;
            int totalPages;

            try
            {
                var service = new StandardsApplicableService(new Repository());
                standardsApplicable = service.BySolution(solutionId);
                standardsApplicable = service.GetPagingValues(pageIndex, pageSize, standardsApplicable, out totalPages);

                if (standardsApplicable == null)
                    return StatusCode(404);
            }
            catch (Crm.CrmApiException ex)
            {
                return StatusCode((int)ex.HttpStatus, ex.Message);
            }

            return new ObjectResult(new PaginatedListStandardsApplicable()
            {
                Items = standardsApplicable.ToList(),
                TotalPages = totalPages,
                PageIndex = pageIndex ?? Paging.DefaultIndex,
                PageSize = pageSize ?? Paging.DefaultPageSize
            });
        }

        /// <summary>
        /// Delete an existing claimed standard for a solution
        /// </summary>

        /// <param name="claimedstandard">existing claimed standard information</param>
        /// <response code="200">Success</response>
        /// <response code="404">ClaimedStandard not found in CRM</response>
        [HttpDelete]
        [Route("/api/StandardsApplicable")]
        [ValidateModelState]
        [SwaggerOperation("ApiStandardsApplicableDelete")]
        public virtual IActionResult ApiStandardsApplicableDelete([FromBody]StandardApplicable claimedstandard)
        {
            try
            {
                var svc = new StandardsApplicableService(new Repository());
                var claimedstandardGet = svc.ById(claimedstandard.Id.ToString());

                if (claimedstandardGet.Id == Guid.Empty)
                    return StatusCode(404);

                svc.Delete(claimedstandard);
            }
            catch (Crm.CrmApiException ex)
            {
                return StatusCode((int)ex.HttpStatus, ex.Message);
            }

            return StatusCode(200);
        }

        /// <summary>
        /// Create a new claimed standard for a solution
        /// </summary>

        /// <param name="claimedstandard">new claimed standard information</param>
        /// <response code="200">Success</response>
        /// <response code="404">Solution not found in CRM</response>
        [HttpPost]
        [Route("/api/StandardsApplicable")]
        [ValidateModelState]
        [SwaggerOperation("ApiStandardsApplicablePost")]
        [SwaggerResponse(statusCode: 200, type: typeof(StandardApplicable), description: "Success")]
        public virtual IActionResult ApiStandardsApplicablePost([FromBody]StandardApplicable claimedstandard)
        {
            try
            {
                claimedstandard = new StandardsApplicableService(new Repository()).Create(claimedstandard);
            }
            catch (Crm.CrmApiException ex)
            {
                return StatusCode((int)ex.HttpStatus, ex.Message);
            }

            return new ObjectResult(claimedstandard);
        }

        /// <summary>
        /// Update an existing claimed standard with new information
        /// </summary>

        /// <param name="claimedstandard">claimed standard with updated information</param>
        /// <response code="200">Success</response>
        /// <response code="404">Solution or ClaimedStandard not found in CRM</response>
        [HttpPut]
        [Route("/api/StandardsApplicable")]
        [ValidateModelState]
        [SwaggerOperation("ApiStandardsApplicablePut")]
        public virtual IActionResult ApiStandardsApplicablePut([FromBody]StandardApplicable claimedstandard)
        {
            try
            {
                new StandardsApplicableService(new Repository()).Update(claimedstandard);
            }
            catch (Crm.CrmApiException ex)
            {
                return StatusCode((int)ex.HttpStatus, ex.Message);
            }

            return StatusCode(200, claimedstandard);
        }
    }
}
