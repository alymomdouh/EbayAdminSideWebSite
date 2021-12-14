namespace EbayView.Models.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetPaginationResult<T>
    {
        public int PageSize { get; set; }
        public int PagNumber { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
        public T Data { get; set; }
    }
}
