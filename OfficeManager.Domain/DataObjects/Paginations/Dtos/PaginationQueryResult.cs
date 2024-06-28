namespace OfficeManager.Domain.DataObjects.Paginations.Dtos
{
    public class PaginationQueryResult<T>
    {
        public int TotalItemCount { get; private set; }
        public IEnumerable<T> QueriedItems { get; private set; }

        public PaginationQueryResult(IEnumerable<T> queriedItems, int totalItemCount)
        {
            TotalItemCount = totalItemCount;
            QueriedItems = queriedItems;
        }
    }
}
