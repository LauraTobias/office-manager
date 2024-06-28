using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeManager.Domain.DataObjects.Documents.Requests;
using OfficeManager.Domain.DataObjects.Documents.Responses;
using OfficeManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OfficeManager.Api.Controllers.v1
{
    [ApiController]
    [Route("/v1/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <summary>
        /// Process to get all documents by case id
        /// </summary>
        /// <returns> Documents with the given case id</returns>
        [ProducesResponseType(typeof(List<DocumentResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/documents/case/{caseId:guid}")]
        public async Task<IActionResult> GetAllByCaseId([FromRoute] Guid caseId)
        {
            var result = await _documentService.GetAllByCaseId(caseId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get the document by id
        /// </summary>
        /// <returns> Document with the given id</returns>
        [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
        [HttpGet("/v1/documents/{documentId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid documentId)
        {
            var result = await _documentService.GetById(documentId);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Process to get all the documents
        /// </summary>
        /// <returns> All the documents/returns>
        [ProducesResponseType(typeof(List<DocumentResponse>), StatusCodes.Status200OK)]
        [HttpGet("/v1/documents")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _documentService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Process to upload a new document
        /// </summary>
        /// <returns> None </returns>
        [HttpPost("/v1/documents/upload/case/{caseId:guid}")]
        public async Task<ActionResult<DocumentResponse>> Upload([FromBody] IFormFile file, [FromRoute] Guid caseId)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("File is invalid");
                }

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine("C:\\DocumentStorage", Guid.NewGuid().ToString() + Path.GetExtension(fileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var request = new DocumentRequest(caseId, fileName, filePath);

                await _documentService.Create(request);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Process to delete a document
        /// </summary>
        /// <returns> None </returns>
        [HttpDelete("/v1/documents")]
        public async Task<IActionResult> Delete([FromRoute] Guid documentId)
        {
            try
            {
                await _documentService.Remove(documentId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
