using FluentValidation;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Resources;

namespace OfficeManager.Domain.Validators
{
    public class ClientMeetingValidator : AbstractValidator<ClientMeeting>
    {
        public ClientMeetingValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Resource.ClientMeetingIdIsEmpty);

            RuleFor(x => x.MeetingDate)
                .NotEmpty()
                .WithMessage(Resource.ClientMeetingMeetingDateIsEmpty);

            RuleFor(x => x.Description)
                 .NotEmpty()
                 .WithMessage(Resource.ClientMeetingDescriptionIsEmpty);

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage(Resource.ClientMeetingClientIdIsEmpty);
        }
    }
}
