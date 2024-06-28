using OfficeManager.Domain.DataObjects.Cases.Responses;
using OfficeManager.Domain.DataObjects.Users.Responses;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Enums;

namespace OfficeManager.Domain.DataObjects.Fees.Responses
{
    public class FeeResponse
    {
        public CaseResponse Case { get; private set; }
        public UserResponse User { get; private set; }
        public double Amount { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }

        public static FeeResponse BuildWith(Fee fee)
        {
            return new FeeResponse
            {
                Amount = fee.Amount,
                PaymentDate = fee.PaymentDate,
                PaymentStatus = fee.PaymentStatus,
                Case = CaseResponse.BuildWith(fee.Case),
                User = UserResponse.BuildWith(fee.User)
            };
        }
    }
}
