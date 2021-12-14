namespace EbayAdminModels.Pagination
{
    public class PaginationResult
    {
        public int PageSize { get; set; }
        public int PagNumber { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
        public object Data { get; set; }
    }
}
