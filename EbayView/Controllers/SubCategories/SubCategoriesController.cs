namespace EbayView.Controllers.SubCategories
{
    using AutoMapper;
    using EbayAdminModels.SubCategory;
    using EbayView.Models.ViewModel.SubCategory;
    using global::Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoryRepository _SubcategoryRepository;
        private readonly IMapper _mapper;
        public SubCategoriesController(ISubCategoryRepository SubcategoryRepository, IMapper mapper)
        {
            _SubcategoryRepository = SubcategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var Subcategories = await _SubcategoryRepository.GetSubCategoriesAsync();
            var result = _mapper.Map<List<GetSubCategoriesOutputModel>>(Subcategories);
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var Subcategory = await _SubcategoryRepository.GetSubCategoryDetailsAsync(id);
            var result = _mapper.Map<GetSubCategoryDetailsOutputModel>(Subcategory);
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CreateSubCategoryInputModel model)
        {
            try
            {
                var Subcategory = _mapper.Map<SubCategory>(model);
                await _SubcategoryRepository.AddSubCategoryAsync(Subcategory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            var Subcategory = await _SubcategoryRepository.GetSubCategoryDetailsAsync(id);
            var result = _mapper.Map<GetSubCategoryDetailsOutputModel>(Subcategory);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateSubCategoryInputModel model)
        {
            try
            {
                var Subcategory = _mapper.Map<SubCategory>(model);
                await _SubcategoryRepository.UpdateSubCategoryAsync(Subcategory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var Subcategory = await _SubcategoryRepository.GetSubCategoryDetailsAsync(id);
            var result = _mapper.Map<GetSubCategoryDetailsOutputModel>(Subcategory);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            try
            {
                var Subcategory = await _SubcategoryRepository.GetSubCategoryDetailsAsync(id);
                await _SubcategoryRepository.DeleteSubCategoryAsync(Subcategory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
             
        }
    }
}
