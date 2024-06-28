using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Domain.DataObjects.ClientMeetings.Requests;
using OfficeManager.Domain.DataObjects.ClientMeetings.Responses;
using OfficeManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeManager.Api.Controllers.v1
{
    [ApiController]
    [Route("/v1/client-meetings")]
    public class ClientMeetingsController : ControllerBase
    {
        private readonly IClientMeetingService _clientMeetingService;

        public ClientMeetingsController(IClientMeetingService clientMeetingService)
        {
            _clientMeetingService = clientMeetingService;
        }

        /// <summary>
        /// Process to get the client meeting by id
        /// </summary>
        /// <returns> Client meeting with the given id</returns>
        [ProducesResponseType(typeof(ClientMeetingResponse), StatusCodes.Status200OK)]
        [HttpGet("/v1/client-meetings/{clientMeetingId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid clientMeetingId)
        {
            var result = await _clientMeetingService.GetById(clientMeetingId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get all the client meetings
        /// </summary>
        /// <returns> All the client meetings</returns>
        [ProducesResponseType(typeof(List<ClientMeetingResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/client-meetings")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _clientMeetingService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Process to get all the client meetings by client id
        /// </summary>
        /// <returns> All the client meetings with the given client id</returns>
        [ProducesResponseType(typeof(List<ClientMeetingResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/client-meetings/client/{clientId:guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid clientId)
        {
            var entities = await _clientMeetingService.GetAllByClientId(clientId);
            return Ok(entities);
        }

        /// <summary>
        /// Process to create a new client meeting
        /// </summary>
        /// <returns> None </returns>
        [HttpPost("/v1/client-meetings")]
        public async Task<IActionResult> Create([FromBody] ClientMeetingRequest request)
        {
            try
            {
                await _clientMeetingService.Create(request);
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
        /// Process to update a client meeting
        /// </summary>
        /// <returns> None </returns>
        [HttpPut("/v1/client-meetings")]
        public async Task<IActionResult> Update([FromBody] ClientMeetingRequest request)
        {
            try
            {
                await _clientMeetingService.Update(request);
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
        /// Process to delete a client meeting
        /// </summary>
        /// <returns> None </returns>
        [HttpDelete("/v1/client-meetings")]
        public async Task<IActionResult> Delete([FromRoute] Guid clientMeetingId)
        {
            try
            {
                await _clientMeetingService.Remove(clientMeetingId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
