using OfficeManager.Domain.DataObjects.Users.Requests;

namespace OfficeManager.Domain.DataObjects.Offices.Requests
{
    public class OfficeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public List<UserRequest> Users { get; set; }
    }
}
