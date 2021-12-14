namespace EbayView.Controllers.Admins
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Category;
    using EbayView.Models.ViewModel.admns;
    using global::Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EbayView.Models.ViewModel.Account;

    public class AdminMapper : Profile
    {
        public AdminMapper()
        {
            CreateMap<CreateAdminInputModel, Admin>();
            CreateMap<PostLoginModel, Admin>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s.Email))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<Admin, GetAdminsOutputModel>().ReverseMap();
            CreateMap<Admin, GetAdminDetailsOutputModel>().ReverseMap();

        }
    }
}
