namespace EbayView.Controllers.SubCategories
{
    using AutoMapper;
    using EbayView.Models.ViewModel.SubCategory;
    using global::Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubCategoryMapper : Profile
    {
        public SubCategoryMapper()
        {
            CreateMap<CreateSubCategoryInputModel, SubCategory>();
            CreateMap<SubCategory, GetSubCategoriesOutputModel>();
            CreateMap<SubCategory, GetSubCategoryDetailsOutputModel>();



        }
    }
}
