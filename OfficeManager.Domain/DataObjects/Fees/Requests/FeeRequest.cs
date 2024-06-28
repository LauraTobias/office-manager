using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Enums;

namespace OfficeManager.Domain.DataObjects.Fees.Requests
{
    public class FeeRequest
    {
        public Guid CaseId { get; set; }
        public Guid UserId { get; set; }
        public double Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
