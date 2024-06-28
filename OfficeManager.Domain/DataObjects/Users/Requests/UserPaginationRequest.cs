using OfficeManager.Domain.DataObjects.Paginations.Requests;

namespace OfficeManager.Domain.DataObjects.Users.Requests
{
    public class UserPaginationRequest : PaginationRequest
    {
        public Guid OfficeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
