namespace OfficeManager.Domain.DataObjects.Users.Requests
{
    public class UserRequest 
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool IsAdmin { get; private set; }
        public Guid OfficeId { get; private set; }
    }
}
