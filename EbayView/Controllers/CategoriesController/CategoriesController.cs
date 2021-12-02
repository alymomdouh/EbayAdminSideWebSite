namespace EbayView.Controllers.CategoriesController
{
    using AutoMapper;
    using EbayAdminModels.Category;
    using EbayView.Models.ViewModel.Category;
    using global::Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
        
            var categories = await _categoryRepository.GetCategoriesAsync();
            var result = _mapper.Map<List<GetCategoriesOutputModel>>(categories);
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryRepository.GetCategoryDetailsAsync(id);
            var result = _mapper.Map<GetCategoryDetailsOutputModel>(category);
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]CreateCategoryInputModel model)
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
        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetCategoryDetailsAsync(id);
            var result = _mapper.Map<CreateCategoryInputModel>(category);
            return View(result);
        }

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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int id)
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
