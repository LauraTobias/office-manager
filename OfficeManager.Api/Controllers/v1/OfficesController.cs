using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Domain.DataObjects.Offices.Requests;
using OfficeManager.Domain.DataObjects.Offices.Responses;
using OfficeManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeManager.Api.Controllers.v1
{
    [ApiController]
    [Route("/v1/offices")]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficesController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        /// <summary>
        /// Process to get the office by id
        /// </summary>
        /// <returns> Office with the given id</returns>
        [ProducesResponseType(typeof(OfficeResponse), StatusCodes.Status200OK)]
        [HttpGet("/v1/offices/{officeId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid officeId)
        {
            var result = await _officeService.GetById(officeId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get all the offices
        /// </summary>
        /// <returns> All the offices</returns>
        [ProducesResponseType(typeof(List<OfficeResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/offices")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _officeService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Process to create a new office
        /// </summary>
        /// <returns> None </returns>
        [HttpPost("/v1/offices")]
        public async Task<IActionResult> Create([FromBody] OfficeRequest request)
        {
            try
            {
                await _officeService.Create(request);
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
        /// Process to update an office
        /// </summary>
        /// <returns> None </returns>
        [HttpPut("/v1/offices")]
        public async Task<IActionResult> Update([FromBody] OfficeRequest request)
        {
            try
            {
                await _officeService.Update(request);
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
        /// Process to delete an office
        /// </summary>
        /// <returns> None </returns>
        [HttpDelete("/v1/offices")]
        public async Task<IActionResult> Delete([FromRoute] Guid officeId)
        {
            try
            {
                await _officeService.Remove(officeId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
