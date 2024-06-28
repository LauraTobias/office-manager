using OfficeManager.Domain.DataObjects.Clients.Requests;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Validators;

namespace OfficeManager.Domain.Entities
{
    public class Client : EntityBase
    {
        public string Cpf { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public List<ClientMeeting> Meetings { get; private set; }

        public static Client BuildWith(ClientRequest request)
        {
            return new Client
            {
                Cpf = request.Cpf,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                OfficeId = request.OfficeId,
            };
        }

        public void Update(Client client)
        {
            Cpf = client.Cpf;
            Name = client.Name;
            Email = client.Email;
            Phone = client.Phone;
            Address = client.Address;
        }

        public override bool IsValid()
        {
            var validator = new ClientValidator();
            var result = validator.Validate(this);

            AddErrors(result.Errors.Select(x => x.ErrorMessage).ToArray());

            return Errors.Count == 0;
        }
    }
}
