using OfficeManager.Domain.DataObjects.Paginations.Requests;

namespace OfficeManager.Domain.DataObjects.Clients.Requests
{
    public class ClientPaginationRequest : PaginationRequest
    {
        public Guid OfficeId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
    }
}
