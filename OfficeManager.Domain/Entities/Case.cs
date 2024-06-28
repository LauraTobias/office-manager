using OfficeManager.Domain.DataObjects.Cases.Requests;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Enums;
using OfficeManager.Domain.Validators;

namespace OfficeManager.Domain.Entities
{
    public class Case : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public CaseStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ConclusionDate { get; set; }

        public static Case BuildWith(CaseRequest request)
        {
            return new Case
            {
                Name = request.Name,
                ClientId = request.ClientId,
                CreationDate = DateTime.Now,
                Description = request.Description,
                Status = request.Status ?? CaseStatus.New,
            };
        }

        public void Update(Case caseEntity)
        {
            Name = caseEntity.Name;
            Status = caseEntity.Status;
            ClientId = caseEntity.ClientId;
            Description = caseEntity.Description;
        }

        public override bool IsValid()
        {
            var validator = new CaseValidator();
            var result = validator.Validate(this);

            AddErrors(result.Errors.Select(x => x.ErrorMessage).ToArray());

            return Errors.Count == 0;
        }
    }
}
