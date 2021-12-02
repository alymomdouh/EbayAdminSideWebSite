namespace EbayView.Controllers.Stocks
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Stocks;
    using global::Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class StocksController : Controller
    {
        private readonly IStockRepository _StockRepository;
        private readonly IMapper _mapper;
        public StocksController(IStockRepository StockRepository, IMapper mapper)
        {
            _StockRepository = StockRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Stocks = await _StockRepository.GetStockAsync();

            var result = _mapper.Map<List<GetStocksOutputModel>>(Stocks);

            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var Stock = await _StockRepository.GetStockDetailsAsync(id);
            var result = _mapper.Map<GetStockDetailsOutputModel>(Stock);
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CreateStockInputModel model)
        {
            try
            {
                var Stock = _mapper.Map<Stock>(model);
                await _StockRepository.AddStockAsync(Stock);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromBody] CreateStockInputModel model)
        {
            try
            {
                var Stock = _mapper.Map<Stock>(model);
                await _StockRepository.UpdateStockAsync(Stock);
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
            var Stock = await _StockRepository.GetStockDetailsAsync(id);
            var result = _mapper.Map<GetStockDetailsOutputModel>(Stock);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int id)
        {
            try
            {
                var stock = await _StockRepository.GetStockDetailsAsync(id);
                await _StockRepository.DeleteStockAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
