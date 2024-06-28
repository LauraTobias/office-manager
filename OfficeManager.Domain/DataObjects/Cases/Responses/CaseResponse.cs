using OfficeManager.Domain.DataObjects.Clients.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Enums;

namespace OfficeManager.Domain.DataObjects.Cases.Responses
{
    public class CaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ClientResponse Client { get; set; }
        public CaseStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ConclusionDate { get; set; }

        public static CaseResponse BuildWith(Case caseEntity)
        {
            return new CaseResponse
            {
                Id = caseEntity.Id,
                Name = caseEntity.Name,
                Description = caseEntity.Description,
                Status = caseEntity.Status,
                CreationDate = caseEntity.CreationDate,
                ConclusionDate = caseEntity.ConclusionDate,
                Client = ClientResponse.BuildWith(caseEntity.Client)
            };
        }
    }
}
