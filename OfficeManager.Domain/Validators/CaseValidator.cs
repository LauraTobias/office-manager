using FluentValidation;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Resources;

namespace OfficeManager.Domain.Validators
{
    public class CaseValidator : AbstractValidator<Case>
    {
        public CaseValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Resource.CaseIdIsEmpty);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Resource.CaseNameIsEmpty);

            RuleFor(x => x.Description)
                 .NotEmpty()
                 .WithMessage(Resource.CaseDescriptionIsEmpty);

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage(Resource.CaseClientIdIsEmpty);

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage(Resource.CaseStatusIsEmpty);
        }
    }
}
