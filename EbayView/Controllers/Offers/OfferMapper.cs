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
            CreateMap<Offers, GetOfferOutputModel>();
            CreateMap<Offers, GetOfferDetailsOutputModel>();

        }
    }
}
