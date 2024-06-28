using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Domain.DataObjects.Cases.Requests;
using OfficeManager.Domain.DataObjects.Cases.Responses;
using OfficeManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeManager.Api.Controllers.v1
{
    [ApiController]
    [Route("/v1/cases")]
    public class CasesController : ControllerBase
    {
        private readonly ICaseService _caseService;

        public CasesController(ICaseService caseService)
        {
            _caseService = caseService;
        }

        /// <summary>
        /// Process to get the case by id
        /// </summary>
        /// <returns> Client meeting with the given id</returns>
        [ProducesResponseType(typeof(List<CaseResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/cases:search")]
        public async Task<IActionResult> GetAlltByClientId([FromBody] CaseRequest request)
        {
            var result = await _caseService.GetAllWithFilters(request);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get the case by id
        /// </summary>
        /// <returns> Client meeting with the given id</returns>
        [ProducesResponseType(typeof(CaseResponse), StatusCodes.Status200OK)]
        [HttpGet("/v1/cases/{caseId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid caseId)
        {
            var result = await _caseService.GetById(caseId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get all the cases
        /// </summary>
        /// <returns> All the cases</returns>
        [ProducesResponseType(typeof(List<CaseResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/cases")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _caseService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Process to create a new case
        /// </summary>
        /// <returns> None </returns>
        [HttpPost("/v1/cases")]
        public async Task<IActionResult> Create([FromBody] CaseRequest request)
        {
            try
            {
                await _caseService.Create(request);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Process to update a case
        /// </summary>
        /// <returns> None </returns>
        [HttpPut("/v1/cases")]
        public async Task<IActionResult> Update([FromBody] CaseRequest request)
        {
            try
            {
                await _caseService.Update(request);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Process to delete a case
        /// </summary>
        /// <returns> None </returns>
        [HttpDelete("/v1/cases")]
        public async Task<IActionResult> Delete([FromRoute] Guid caseId)
        {
            try
            {
                await _caseService.Remove(caseId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
