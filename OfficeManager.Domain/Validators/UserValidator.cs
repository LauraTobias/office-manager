using FluentValidation;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Resources;

namespace OfficeManager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Resource.UserIdIsEmpty);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Resource.UserNameIsEmpty);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Resource.UserEmailIsEmpty);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Resource.UserPasswordIsEmpty);

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage(Resource.UserBirthDateIsEmpty);

            RuleFor(x => x.Profile)
                 .NotEmpty()
                 .WithMessage(Resource.UserProfileIsEmpty);

            RuleFor(x => x.OfficeId)
                .NotEmpty()
                .WithMessage(Resource.UserOfficeIdIsEmpty);
        }
    }
}