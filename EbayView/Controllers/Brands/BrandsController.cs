namespace EbayView.Controllers.Brands
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Brands;
    using global::Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class BrandsController : Controller
    {
        private readonly IBrandRepository _BrandRepository;
        private readonly IMapper _mapper;
        public BrandsController(IBrandRepository BrandRepository, IMapper mapper)
        {
            _BrandRepository = BrandRepository;
            _mapper = mapper;
        }
        // GET: BrandsController
        public async Task<IActionResult> Index()
        {
            var Brands = await _BrandRepository.GetBrandsAsync();

            var result = _mapper.Map<List<GetBrandsOutputModel>>(Brands);

            return View(result);
        }

        // GET: BrandsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var Brand = await _BrandRepository.GetBrandDetailsAsync(id);
            var result = _mapper.Map<GetBrandDetailsOutputModel>(Brand);
            return View(result);
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: BrandsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CreateBrandInputModel model)
        {
            try
            {
                var Brand = _mapper.Map<Brands>(model);
                await _BrandRepository.AddBrandAsync(Brand);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromBody] CreateBrandInputModel model)
        {
            try
            {
                var Brand = _mapper.Map<Brands>(model);
                await _BrandRepository.UpdateBrandAsync(Brand);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BrandsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var stock = await _BrandRepository.GetBrandDetailsAsync(id);
                await _BrandRepository.DeleteBrandAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
