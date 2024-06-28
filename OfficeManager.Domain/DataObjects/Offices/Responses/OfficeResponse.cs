using OfficeManager.Domain.DataObjects.Users.Responses;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.DataObjects.Offices.Responses
{
    public class OfficeResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public List<UserResponse> Users { get; private set; }

        public static OfficeResponse BuildWith(Office office)
        {
            return new OfficeResponse
            {
                Id = office.Id,
                Cnpj = office.Cnpj,
                Name = office.Name,
                Users = office.Users
                .Select(x => UserResponse.BuildWith(x))
                .ToList()
            };
        }
    }
}
