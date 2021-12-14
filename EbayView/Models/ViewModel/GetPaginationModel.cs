namespace EbayView.Models.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetPaginationModel
    {
        public int PageSize { get; set; } = 10;
        public int PagNumber { get; set; } = 1;
    }
}
