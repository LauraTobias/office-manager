using OfficeManager.Domain.DataObjects.Users.Requests;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Enums;
using OfficeManager.Domain.Validators;
using System.Security.Cryptography;
using System.Text;

namespace OfficeManager.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Profile Profile { get; private set; }
        public Guid OfficeId { get; private set; }
        public Office Office { get; private set; }

        public static User BuildWith(UserRequest request)
        {
            return new User
            {
                Password = HashPassword(request.Password),
                BirthDate = request.BirthDate,
                OfficeId = request.OfficeId,
                Email = request.Email,
                Name = request.Name,
                Profile = request.IsAdmin 
                ? Profile.Administrator 
                : Profile.Employee
            };
        }

        public void Update(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Profile = user.Profile;
            OfficeId = user.OfficeId;
            BirthDate = user.BirthDate;
            Password = HashPassword(user.Password);
        }

        public void ResetPassword(string password)
        {
            Password = HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return Password == HashPassword(password);
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public override bool IsValid()
        {
            var validator = new UserValidator();
            var result = validator.Validate(this);

            AddErrors(result.Errors.Select(x => x.ErrorMessage).ToArray());

            return Errors.Count == 0;
        }
    }
}
