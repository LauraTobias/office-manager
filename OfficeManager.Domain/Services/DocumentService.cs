using OfficeManager.Domain.DataObjects.Documents.Requests;
using OfficeManager.Domain.DataObjects.Documents.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Interfaces.Repositories;
using OfficeManager.Domain.Interfaces.Services;

namespace OfficeManager.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ICaseService _caseService;

        public DocumentService(
            IDocumentRepository documentRepository, 
            ICaseService caseService)
        {
            _documentRepository = documentRepository;
            _caseService = caseService;
        }

        public async Task Create(DocumentRequest request)
        {
            var document = Document.BuildWith(request);

            await CheckIfDependenciesExist(document);

            if (!document.IsValid())
            {
                throw new ArgumentException(string.Join(", ", document.Errors));
            }

            await _documentRepository.AddAsync(document);

            await _documentRepository.Commit();
        }

        public async Task<bool> Exists(Guid documentId)
        {
            return await _documentRepository.ExistsAsync(documentId);
        }

        public async Task<List<DocumentResponse>> GetAll()
        {
            var result = await _documentRepository.GetAllWithDependencies();

            return result.Select(x => DocumentResponse.BuildWith(x)).ToList();
        }

        public async Task<List<DocumentResponse>> GetAllByCaseId(Guid caseId)
        {
            var result = await _documentRepository.GetAllByCaseIdAsync(caseId);

            return result.Select(x => DocumentResponse.BuildWith(x)).ToList();
        }

        public async Task<DocumentResponse> GetById(Guid documentId)
        {
            var result = await _documentRepository.GetByIdWithDependencies(documentId);

            if (result is null)
            {
                throw new KeyNotFoundException("Document not found");
            }

            return DocumentResponse.BuildWith(result);
        }

        public async Task Remove(Guid documentId)
        {
            await _documentRepository.RemoveAsync(documentId);

            await _documentRepository.Commit();
        }

        private async Task CheckIfDependenciesExist(Document document)
        {
            var caseExists = await _caseService.Exists(document.CaseId);

            if (!caseExists)
            {
                throw new KeyNotFoundException("Case not found");
            }
        }
    }
}
