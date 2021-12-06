﻿namespace EbayView.Controllers.Offers
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Offers;
    using global::Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class OffersController : Controller
    {
        private readonly IOfferRepository _OfferRepository;
        private readonly IMapper _mapper;
        public OffersController(IOfferRepository OfferRepository, IMapper mapper)
        {
            _OfferRepository = OfferRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Offers = await _OfferRepository.GetOffersAsync();

            var result = _mapper.Map<List<GetOfferOutputModel>>(Offers);

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var Offer = await _OfferRepository.GetOfferDetailsAsync(id);
            var result = _mapper.Map<GetOfferDetailsOutputModel>(Offer);
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
            var Offer = await _OfferRepository.GetOfferDetailsAsync(id);
            var result = _mapper.Map<GetOfferDetailsOutputModel>(Offer);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOffersInputModel model)
        {
            try
            {
                var Offer = _mapper.Map<Offers>(model);
                await _OfferRepository.AddOfferAsync(Offer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateOffersInputModel model)
        {
            try
            {
                var Offer = _mapper.Map<Offers>(model);
                await _OfferRepository.UpdateOfferAsync(Offer);
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
            Offers Offer = await _OfferRepository.GetOfferDetailsAsync(id);
            var result = _mapper.Map<GetOfferDetailsOutputModel>(Offer);

            return View(result);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int id)
        {
            try
            {
                var Offer = await _OfferRepository.GetOfferDetailsAsync(id);
                await _OfferRepository.DeleteOfferAsync(Offer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
