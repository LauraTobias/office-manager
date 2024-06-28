using OfficeManager.Domain.Enums;

namespace OfficeManager.Domain.DataObjects.Cases.Requests
{
    public class CaseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ClientId { get; set; }
        public CaseStatus? Status { get; set; }
    }
}
