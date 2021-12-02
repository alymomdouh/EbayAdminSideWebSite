namespace EbayView.Controllers.Products
{
    using AutoMapper;
    using EbayAdminModels.Category;
    using EbayView.Models.ViewModel.Brands;
    using EbayView.Models.ViewModel.Category;
    using EbayView.Models.ViewModel.Products;
    using EbayView.Models.ViewModel.Stocks;
    using global::Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductsController(IProductRepository productRepository, IMapper mapper
            ,ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _stockRepository = stockRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetProductsAsync();
            var result = _mapper.Map<List<GetProductsOutputModel>>(products);

            return View(result);
        }
        [HttpGet("cre")]
        public async Task<ActionResult> Create()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var categoriesResult = _mapper.Map<List<GetCategoriesOutputModel>>(categories);

            var brands = await _brandRepository.GetBrandsAsync();
            var brandsResult = _mapper.Map<List<GetBrandsOutputModel>>(brands);

            var stocks = await _stockRepository.GetStockAsync();
            var stocksResult = _mapper.Map<List<GetStocksOutputModel>>(stocks);
            GetMetaDataOutputModel metaData = new GetMetaDataOutputModel();
            metaData.Brands = brandsResult;
            metaData.Categories = categoriesResult;
            metaData.Stocks = stocksResult;
            return View(metaData);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CreateProductInputModel model)
        {
            var product = _mapper.Map<Product>(model);

            await _productRepository.AddProductAsync(product);

            return RedirectToAction("Index");
        }
        [HttpGet()]
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
            return View(result);
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
