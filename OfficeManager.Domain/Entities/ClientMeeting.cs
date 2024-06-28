using OfficeManager.Domain.DataObjects.ClientMeetings.Requests;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Validators;

namespace OfficeManager.Domain.Entities
{
    public class ClientMeeting : EntityBase
    {
        public DateTime MeetingDate { get; private set; }
        public string Description { get; private set; }
        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }

        public static ClientMeeting BuildWith(ClientMeetingRequest request)
        {
            return new ClientMeeting
            {
                MeetingDate = request.MeetingDate,
                Description = request.Description,
                ClientId = request.ClientId
            };
        }

        public void Update(ClientMeeting clientMeeting)
        {
            MeetingDate = clientMeeting.MeetingDate;
            Description = clientMeeting.Description;
            ClientId = clientMeeting.ClientId;
        }

        public override bool IsValid()
        {
            var validator = new ClientMeetingValidator();
            var result = validator.Validate(this);

            AddErrors(result.Errors.Select(x => x.ErrorMessage).ToArray());

            return Errors.Count == 0;
        }
    }
}
