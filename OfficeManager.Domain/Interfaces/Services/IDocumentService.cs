using OfficeManager.Domain.DataObjects.Documents.Requests;
using OfficeManager.Domain.DataObjects.Documents.Responses;

namespace OfficeManager.Domain.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<List<DocumentResponse>> GetAllByCaseId(Guid caseId);
        Task<DocumentResponse> GetById(Guid documentId);
        Task Create(DocumentRequest documentRequest);
        Task<List<DocumentResponse>> GetAll();
        Task<bool> Exists(Guid documentId);
        Task Remove(Guid documentId);
    }
}
