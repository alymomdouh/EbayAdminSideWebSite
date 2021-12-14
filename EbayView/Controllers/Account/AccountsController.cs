namespace EbayView.Controllers.Account
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Account;
    using global::Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class AccountsController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        private readonly IMapper _mapper;
        public AccountsController( IMapper mapper, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        [HttpGet, ActionName("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Login")]

        public async Task<IActionResult> Login(PostLoginModel model)
        { 
            var admin = await _adminRepository.GetAdminAsync(model.UserName, model.Password);
            if (admin is null)
            {
                RedirectToAction("register");
            }
            return RedirectToAction("Index", "Products");
        }
        public IActionResult register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("register")]
        public async Task<IActionResult> register(PostLoginModel model)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                return RedirectToAction(nameof(Login));
            }
        }

    }
}
