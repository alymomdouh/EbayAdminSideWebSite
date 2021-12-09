﻿namespace EbayView.Controllers.Admins
{
    using AutoMapper;
    using EbayView.Models.ViewModel.admns;
    using global::Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class AdminsController : Controller
    {
        private readonly IAdminRepository _AdminRepository;
        private readonly IMapper _mapper;
        public AdminsController(IAdminRepository AdminRepository, IMapper mapper)
        {
            _AdminRepository = AdminRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Admins = await _AdminRepository.GetAdminAsync();

            var result = _mapper.Map<List<GetAdminsOutputModel>>(Admins);

            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var Admin = await _AdminRepository.GetAdminDetailsAsync(id);
            var result = _mapper.Map<GetAdminDetailsOutputModel>(Admin);
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Admin = await _AdminRepository.GetAdminDetailsAsync(id);
            var result = _mapper.Map<GetAdminDetailsOutputModel>(Admin);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminInputModel model)
        {
            try
            {
                var Admin = _mapper.Map<Admin>(model);
                await _AdminRepository.AddAdminAsync(Admin);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(CreateAdminInputModel model)
        {
            try
            {
                var Admin = _mapper.Map<Admin>(model);
                await _AdminRepository.UpdateAdminAsync(Admin);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var Admin = await _AdminRepository.GetAdminDetailsAsync(id);
            var result = _mapper.Map<GetAdminDetailsOutputModel>(Admin);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int id)
        {
            try
            {
                var Admin = await _AdminRepository.GetAdminDetailsAsync(id);
                await _AdminRepository.DeleteAdminAsync(Admin);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}