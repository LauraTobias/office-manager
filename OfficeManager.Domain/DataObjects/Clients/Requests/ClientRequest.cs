namespace OfficeManager.Domain.DataObjects.Clients.Requests
{
    public class ClientRequest
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid OfficeId { get; set; }
    }
}
