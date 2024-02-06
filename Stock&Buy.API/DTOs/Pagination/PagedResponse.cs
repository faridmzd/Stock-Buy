namespace Stock_Buy.API.DTOs.Pagination
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }


        private PagedResponse(IEnumerable<T> items, int pageNumber, int pageSize, int totalPages, int totalCount)
        {
            this.Items = items;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
            this.TotalCount = totalCount;
        }


        public static PagedResponse<T> Create(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PagedResponse<T>(items, pageNumber, pageSize, totalPages, totalCount);
        }
    }
}
