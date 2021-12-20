using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayView.Views.Shared.Components.SearchBar
{
    public class SearchBarViewComponent: ViewComponent
    {
        public SearchBarViewComponent() { }
        public IViewComponentResult Invoke(SPager SearchPager,bool SearchNavagtion)
        {
            if (SearchNavagtion) { return View("SearchPart", SearchPager); }
            else { return View("PaginationPart", SearchPager); } 
        }
    }
}
