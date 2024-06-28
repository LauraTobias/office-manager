namespace OfficeManager.Domain.DataObjects.Paginations.Responses
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public PaginationResponse() { }

        public PaginationResponse(IEnumerable<T> data, int currentPage, int totalPages, int totalItems)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalItems = totalItems;
            Data = data;
        }
    }
}
