using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Domain.DataObjects.Clients.Requests;
using OfficeManager.Domain.DataObjects.Clients.Responses;
using OfficeManager.Domain.DataObjects.Paginations.Requests;
using OfficeManager.Domain.DataObjects.Paginations.Responses;
using OfficeManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeManager.Api.Controllers.v1
{
    [ApiController]
    [Route("/v1/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Process to get all clients by office id
        /// </summary>
        /// <returns> Clients with the given office id</returns>
        [ProducesResponseType(typeof(List<ClientResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/clients/office/{officeId:guid}")]
        public async Task<IActionResult> GetAllByCaseId([FromRoute] Guid officeId)
        {
            var result = await _clientService.GetAllByOfficeId(officeId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get the clients paginated
        /// </summary>
        /// <returns> List of clients paged </returns>
        [ProducesResponseType(typeof(PaginationResponse<ClientResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/clients:paginated")]
        public async Task<IActionResult> GetPaginated([FromBody] ClientPaginationRequest request)
        {
            var result = await _clientService.GetPaginated(request);
            return Ok(result);
        }

        /// <summary>
        /// Process to get the client by id
        /// </summary>
        /// <returns> Client with the given id</returns>
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [HttpGet("/v1/clients/{clientId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid clientId)
        {
            var result = await _clientService.GetById(clientId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get all the clients
        /// </summary>
        /// <returns> All the clients</returns>
        [ProducesResponseType(typeof(List<ClientResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/clients")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _clientService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Process to create a new client
        /// </summary>
        /// <returns> None </returns>
        [HttpPost("/v1/clients")]
        public async Task<IActionResult> Create([FromBody] ClientRequest request)
        {
            try
            {
                await _clientService.Create(request);
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
        /// Process to update a client
        /// </summary>
        /// <returns> None </returns>
        [HttpPut("/v1/clients")]
        public async Task<IActionResult> Update([FromBody] ClientRequest request)
        {
            try
            {
                await _clientService.Update(request);
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
        /// Process to delete a client
        /// </summary>
        /// <returns> None </returns>
        [HttpDelete("/v1/clients")]
        public async Task<IActionResult> Delete([FromRoute] Guid clientId)
        {
            try
            {
                await _clientService.Remove(clientId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
