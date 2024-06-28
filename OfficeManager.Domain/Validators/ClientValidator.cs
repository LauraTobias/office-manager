using FluentValidation;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Resources;

namespace OfficeManager.Domain.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Resource.ClientIdIsEmpty);
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Resource.ClientNameIsEmpty);
            
            RuleFor(x => x.Cpf)
                 .NotEmpty()
                 .WithMessage(Resource.ClientCpfIsEmpty);
            
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(Resource.ClientAddressIsEmpty);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(Resource.ClientPhoneIsEmpty);

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage(Resource.ClientEmailIsEmpty);

            RuleFor(x => x.OfficeId)
               .NotEmpty()
               .WithMessage(Resource.ClientOfficeIdIsEmpty);
        }
    }
}
