namespace EbayView.Controllers.CategoriesController
{
    using AutoMapper;
    using EbayAdminModels.Category;
    using EbayView.Models.ViewModel.Category;
    using global::Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        // GET: CategoriesController
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            var result = _mapper.Map<List<GetCategoriesOutputModel>>(categories);

            return View(result);
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryRepository.GetCategoryDetailsAsync(id);
            var result = _mapper.Map<GetCategoryDetailsOutputModel>(category);
            return View(result);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CreateCategoryInputModel model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                await _categoryRepository.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromBody] CreateCategoryInputModel model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                await _categoryRepository.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryDetailsAsync(id);
                await _categoryRepository.DeleteCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
