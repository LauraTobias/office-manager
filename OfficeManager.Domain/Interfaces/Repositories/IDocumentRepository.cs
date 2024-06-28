using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.Interfaces.Repositories
{
    public interface IDocumentRepository : IAsyncRepository<Document>
    {
        Task<Document> GetByIdWithDependencies(Guid documentId);
        Task<List<Document>> GetAllByCaseIdAsync(Guid caseId);
        Task<List<Document>> GetAllWithDependencies();
    }
}
