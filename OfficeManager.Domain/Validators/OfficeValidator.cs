using FluentValidation;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Resources;

namespace OfficeManager.Domain.Validators
{
    public class OfficeValidator : AbstractValidator<Office>
    {
        public OfficeValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Resource.OfficeIdIsEmpty);

            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage(Resource.OfficeNameIsEmpty);

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .WithMessage(Resource.OfficeCnpjIsEmpty);
        }
    }
}