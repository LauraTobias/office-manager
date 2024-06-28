namespace OfficeManager.Domain.DataObjects.ClientMeetings.Requests
{
    public class ClientMeetingRequest
    {
        public DateTime MeetingDate { get; set; }
        public string Description { get; set; }
        public Guid ClientId { get; set; }
    }
}
