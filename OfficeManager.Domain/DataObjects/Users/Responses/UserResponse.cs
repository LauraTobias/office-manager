using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.DataObjects.Users.Responses
{
    public class UserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid OfficeId { get; set; }

        public static UserResponse BuildWith(User user)
        {
            return new UserResponse
            {
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate,
                OfficeId = user.OfficeId,
            };
        }
    }
}
