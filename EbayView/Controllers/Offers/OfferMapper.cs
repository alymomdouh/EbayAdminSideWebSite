namespace EbayView.Controllers.Offers
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Offers;
    using global::Models;

    public class BrandMapper : Profile
    {
        public BrandMapper()
        {
            CreateMap<CreateOffersInputModel, Offers>();
            CreateMap<Offers, GetOfferOutputModel>().ReverseMap();
            CreateMap<Offers, GetOfferDetailsOutputModel>();

            CreateMap<Offers, GetOfferOutputModel>()
                .ForMember(des => des.ProductName, o => o.MapFrom(s => s.product.Name));
            CreateMap<Offers, GetOfferDetailsOutputModel>()
                .ForMember(des => des.OldPrice, o => o.MapFrom(s => s.product.Price)) 
                .ForMember(des => des.AdminId, o => o.MapFrom(s => s.admin.AdminId))
                .ForMember(des => des.AdminName, o => o.MapFrom(s => s.admin.FistName+" "+s.admin.LastName))
                .ForMember(des => des.ProductName, o => o.MapFrom(s => s.product.Name));
        }
    }
}
