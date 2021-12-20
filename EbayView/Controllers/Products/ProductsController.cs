namespace EbayView.Controllers.Products
{
    using AutoMapper;
    using EbayAdminModels.Category;
    using EbayAdminModels.SubCategory;
    using EbayView.Models;
    using EbayView.Models.ViewModel;
    using EbayView.Models.ViewModel.Brands;
    using EbayView.Models.ViewModel.Category;
    using EbayView.Models.ViewModel.Products;
    using EbayView.Models.ViewModel.Stocks;
    using EbayView.Models.ViewModel.SubCategory;
    using global::Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using EbayView.Views.Shared.Components.SearchBar;
    using EbayView.Services;

    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subcategoryRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductsController(IProductRepository productRepository, IMapper mapper
            ,ICategoryRepository categoryRepository, ISubCategoryRepository subcategoryRepository,
            IBrandRepository brandRepository,IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _stockRepository = stockRepository;
            _subcategoryRepository = subcategoryRepository;

        }

        //[HttpGet] // finshed
        public async Task<IActionResult> Index(string SearchText = "",int pg=1, string sortExpression = "")
        {
            //object o = TempData.Peek("admin");
            //ViewBag.admin = (o == null ? null : JsonSerializer.Deserialize<Admin>((string)o));
            //var value = HttpContext.Session.GetString("login"); 
            //if (!string.IsNullOrWhiteSpace(value))
            //{
            // for search
            List<Product> products;
            if (SearchText!=null&&SearchText!="")
            {
                products = await _productRepository.GetProductsAsyncWithSearch(SearchText);
            }
            else
            {
                products = await _productRepository.GetProductsAsync();
            }
            //var products = await _productRepository.GetProductsAsync();
            

            ////make sort by column Name
            // SortModel sortModel = applySort(sortExpression);
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Name", true);
            sortModel.AddColumn("Quantity");
            sortModel.AddColumn("Price");
            sortModel.applySort(sortExpression);
            ViewData["SortModel"] = sortModel;
            //List<Instructor> INS = mycontext.Instructors.ToList();
            //List<Product> INS = await _productRepository.GetProductsAsyncWithSort(sortModel.sortproperty, sortModel.sortOrder);
            //return View(INS);
            if (sortExpression!=""&&sortExpression!=null)
            {
                 products = await _productRepository.GetProductsAsyncWithSort(sortModel.sortproperty, sortModel.sortOrder);
            }
            List<GetProductsOutputModel> result = _mapper.Map<List<GetProductsOutputModel>>(products);


            // merage 
            // SPager searchpager = new SPager() { Action = "Index", Controller = "Products", SearchText = SearchText };
            // ViewBag.SearchPager = searchpager;
            // TempData.Keep("admin");
            // for Pagination
            const int pagesize = 4;
             if (pg < 1) { pg = 1; }
             int recsCount = result.Count();
             //var pager = new Pager(recsCount,pg,pagesize);
             int recSkip = (pg - 1) * pagesize;
             var data = result.Skip(recSkip).Take(pagesize).ToList();
            SPager searchpager = new SPager(recsCount, pg, pagesize) {Action="Index",Controller="Products",SearchText=SearchText };
            ViewBag.SearchPager = searchpager;
             //this.ViewBag.Pager = pager;
            return View(data);
            // to merage search and pager 
            //return View(result);
            //}
            //return RedirectToAction("Login","User");

        }
        [HttpGet]// error
        public async Task<ActionResult> Create()
        {
            // made by aly
            //var Admins = await _categoryRepository.GetCategoriesAsync();
            //var AllAdminsResult = _mapper.Map<List<GetCategoriesOutputModel>>(Admins);
            //ViewBag.AvailableAdmins = AllAdminsResult;

            var categories = await _categoryRepository.GetCategoriesAsync();
            var AllcategoriesResult = _mapper.Map<List<GetCategoriesOutputModel>>(categories);
            ViewBag.AvailableCategories = AllcategoriesResult;

            var subcategories = await _subcategoryRepository.GetSubCategoriesAsync();
            var AllsubcategoriesResult = _mapper.Map<List<GetSubCategoriesOutputModel>>(subcategories);
            ViewBag.AvailableSubCategories = AllsubcategoriesResult;

            var brands = await _brandRepository.GetBrandsAsync();
            var AllbrandsResult = _mapper.Map<List<GetBrandsOutputModel>>(brands);
            ViewBag.AvailableBrands = AllbrandsResult;

            var stocks = await _stockRepository.GetStockAsync();
            var AllstocksResult = _mapper.Map<List<GetStocksOutputModel>>(stocks);
            ViewBag.AvailableStock = AllstocksResult; 
            return View();
        }  
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            
               var product = _mapper.Map<Product>(model); 
            await _productRepository.AddProductAsync(product);
            ///impsrepostory.add(model.imgspathes)
            //return View(product);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return  BadRequest(HttpStatusCode.BadRequest);
            }
            var product = await _productRepository.GetProductDetailsAsync(id.Value);

            if (product == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<GetProductDetailsOutputModel>(product);

            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return  BadRequest(HttpStatusCode.BadRequest);
            }
            var product = await _productRepository.GetProductDetailsAsync(id.Value); 
            if (product == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<GetProductDetailsOutputModel>(product);
            ViewBag.selectProduct = result;

            var categories = await _categoryRepository.GetCategoriesAsync();
            var AllcategoriesResult = _mapper.Map<List<GetCategoriesOutputModel>>(categories);
            ViewBag.AvailableCategories = AllcategoriesResult;

            var subcategories = await _subcategoryRepository.GetSubCategoriesAsync();
            var AllsubcategoriesResult = _mapper.Map<List<GetSubCategoriesOutputModel>>(subcategories);
            ViewBag.AvailableSubCategories = AllsubcategoriesResult;

            var brands = await _brandRepository.GetBrandsAsync();
            var AllbrandsResult = _mapper.Map<List<GetBrandsOutputModel>>(brands);
            ViewBag.AvailableBrands = AllbrandsResult;

            var stocks = await _stockRepository.GetStockAsync();
            var AllstocksResult = _mapper.Map<List<GetStocksOutputModel>>(stocks);
            ViewBag.AvailableStock = AllstocksResult; 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateProductInputModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model);
                await _productRepository.UpdateProductAsync(product);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            } 
            var product = await _productRepository.GetProductDetailsAsync(id.Value); 
            if (product == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<GetProductDetailsOutputModel>(product);

            //return View("producat is deleted");
            // add by aly
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetProductDetailsAsync(id);

             await _productRepository.DeleteProductAsync(product);
            return RedirectToAction("Index");
        }
    }
}
