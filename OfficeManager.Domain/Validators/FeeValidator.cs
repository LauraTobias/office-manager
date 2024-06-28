using FluentValidation;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Enums;
using OfficeManager.Domain.Resources;

namespace OfficeManager.Domain.Validators
{
    public class FeeValidator : AbstractValidator<Fee>
    {
        public FeeValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Resource.FeeIdIsEmpty);

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage(Resource.FeeAmountIsEmpty);

            RuleFor(x => x.PaymentStatus)
                .NotEmpty()
                .WithMessage(Resource.FeePaymentStatusIsEmpty);

            RuleFor(x => x.CaseId)
                .NotEmpty()
                .WithMessage(Resource.FeeCaseIdIsEmpty);

            RuleFor(x => x.UserId)
               .NotEmpty()
               .WithMessage(Resource.FeeUserIdIsEmpty);
        }
    }
}
