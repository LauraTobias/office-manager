using OfficeManager.Domain.DataObjects.Fees.Requests;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Enums;
using OfficeManager.Domain.Validators;

namespace OfficeManager.Domain.Entities
{
    public class Fee : EntityBase
    {
        public Guid CaseId { get; private set; }
        public Case Case { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public double Amount { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }

        public static Fee BuildWith(FeeRequest request)
        {
            return new Fee
            {
                CaseId = request.CaseId,
                UserId = request.UserId,
                Amount = request.Amount,
                PaymentStatus = request.PaymentStatus,
                PaymentDate = request.PaymentStatus == PaymentStatus.Completed 
                ? DateTime.Now 
                : null
            };
        }

        public void Update(Fee fee)
        {
            CaseId = fee.CaseId;
            UserId = fee.UserId;
            Amount = fee.Amount;
            PaymentStatus = fee.PaymentStatus;
            PaymentDate = fee.PaymentStatus == PaymentStatus.Completed
               ? DateTime.Now
               : null;
        }

        public override bool IsValid()
        {
            var validator = new FeeValidator();
            var result = validator.Validate(this);

            AddErrors(result.Errors.Select(x => x.ErrorMessage).ToArray());

            return Errors.Count == 0;
        }
    }
}
