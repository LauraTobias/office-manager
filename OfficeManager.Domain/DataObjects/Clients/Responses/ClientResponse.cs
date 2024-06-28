using OfficeManager.Domain.DataObjects.ClientMeetings.Responses;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Domain.DataObjects.Clients.Responses
{
    public class ClientResponse
    {
        public string Cpf { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public List<ClientMeetingResponse> Meetings { get; private set; }

        public static ClientResponse BuildWith(Client client)
        {
            return new ClientResponse
            {
                Address = client.Address,
                Email = client.Email,
                Phone = client.Phone,
                Name = client.Name,
                Cpf = client.Cpf,
                Meetings = client.Meetings
                .Select(x => ClientMeetingResponse.BuildWith(x))
                .ToList()
            };
        }
    }
}
