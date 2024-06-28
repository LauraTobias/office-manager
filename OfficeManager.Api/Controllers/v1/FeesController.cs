using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Domain.DataObjects.Fees.Requests;
using OfficeManager.Domain.DataObjects.Fees.Responses;
using OfficeManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeManager.Api.Controllers.v1
{
    [ApiController]
    [Route("/v1/fees")]
    public class FeesController : ControllerBase
    {
        private readonly IFeeService _feeService;

        public FeesController(IFeeService feService)
        {
            _feeService = feService;
        }

        /// <summary>
        /// Process to get the fee by id
        /// </summary>
        /// <returns> Fee with the given id</returns>
        [ProducesResponseType(typeof(FeeResponse), StatusCodes.Status200OK)]
        [HttpGet("/v1/fees/{feeId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid feeId)
        {
            var result = await _feeService.GetById(feeId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get all the fees
        /// </summary>
        /// <returns> All the fees</returns>
        [ProducesResponseType(typeof(List<FeeResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/fees")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _feeService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Process to create a new fee
        /// </summary>
        /// <returns> None </returns>
        [HttpPost("/v1/fees")]
        public async Task<IActionResult> Create([FromBody] FeeRequest request)
        {
            try
            {
                await _feeService.Create(request);
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
        /// Process to update a fee
        /// </summary>
        /// <returns> None </returns>
        [HttpPut("/v1/fees")]
        public async Task<IActionResult> Update([FromBody] FeeRequest request)
        {
            try
            {
                await _feeService.Update(request);
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
        /// Process to delete a fee
        /// </summary>
        /// <returns> None </returns>
        [HttpDelete("/v1/fees")]
        public async Task<IActionResult> Delete([FromRoute] Guid feeId)
        {
            try
            {
                await _feeService.Remove(feeId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
