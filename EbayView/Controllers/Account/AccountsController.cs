namespace EbayView.Controllers.Account
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
        [HttpGet, ActionName("Login")]
        public ActionResult Login()
        { 
            return View();
        }
        
        //[ValidateAntiForgeryToken]
        [HttpPost, ActionName("Login")] 
        public async Task<IActionResult> Login(PostLoginModel model)
        {  // this all are error fix it 
            var user = await _UserRepository.GetUserAsync(model.UserName, model.Password);
            if(user is null)
            {
                 RedirectToAction("Create","User");
            }
            //HttpContext.Session.SetString("login", "login");
             return RedirectToAction("Index","Products");
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("register")]
        public IActionResult register(PostLoginModel model)
        {
            try
            {
                // write your code here
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Login));
            }
        }

    }
}
