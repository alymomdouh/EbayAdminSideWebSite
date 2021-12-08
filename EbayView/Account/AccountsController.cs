namespace EbayView.Account
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Account;
    using global::Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
   
    public class AccountsController : Controller
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        public AccountsController(IUserRepository UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Login(PostLoginModel model)
        {
            var user = await _UserRepository.GetUserAsync(model.UserName, model.Password);
            if(user is null)
            {
                RedirectToAction("Create","User");
            }
            HttpContext.Session.SetString("login", "login");
            return RedirectToAction("Index","Products");
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
    }
}
