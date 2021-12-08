namespace EbayView.Controllers.Users
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Category;
    using EbayView.Models.ViewModel.Users;
    using global::Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, GetUsersOutputModel>();
            CreateMap<User, GetUsserDetailsOutputModel>();

        }
    }
}
