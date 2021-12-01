namespace EbayView.Controllers.Products
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Products;
    using global::Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, GetProductsOutputModel>();

            CreateMap<CreateProductInputModel, Product>()
                .ForMember(dest => dest.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(dest => dest.Name, o => o.MapFrom(s => s.Name))
                .ForMember(dest => dest.Price, o => o.MapFrom(s => s.Price))
                .ForMember(dest => dest.Quantity, o => o.MapFrom(s => s.Quantity))
                .ForMember(dest => dest.Description, o => o.MapFrom(s => s.Description))
                .ForMember(dest => dest.AdminId, o => o.MapFrom(s => s.AdminId))
                .ForMember(dest => dest.SubCatId, o => o.MapFrom(s => s.SubCatId))
                .ForMember(dest => dest.StockId, o => o.MapFrom(s => s.StockId))
                .ForMember(dest => dest.BrandId, o => o.MapFrom(s => s.BrandId))
                .ForMember(dest => dest.CatId, o => o.MapFrom(s => s.CatId))
                .ForMember(dest => dest.productImgs, o => o.MapFrom(s => ProductImg.Create(s.imgspathes)))
                .ForAllOtherMembers(dest => dest.Ignore());


            CreateMap<Product, GetProductDetailsOutputModel>()
                .ForMember(dest => dest.AdminName, o => o.MapFrom(s => s.Admin.FistName));
                

        }
    }
}
