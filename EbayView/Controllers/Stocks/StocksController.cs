namespace EbayView.Controllers.Stocks
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Stocks;
    using global::Models;
    using Microsoft.AspNetCore.Http;
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
        // GET: StocksController
        public async Task<IActionResult> Index()
        {
            var Stocks = await _StockRepository.GetStockAsync();

            var result = _mapper.Map<List<GetStocksOutputModel>>(Stocks);

            return View(result);
        }

        // GET: StocksController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var Stock = await _StockRepository.GetStockDetailsAsync(id);
            var result = _mapper.Map<GetStockDetailsOutputModel>(Stock);
            return View(result);
        }

        // GET: StocksController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: StocksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StocksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CreateStockInputModel model)
        {
            try
            {
                var Stock = _mapper.Map<Stocks>(model);
                await _StockRepository.AddStockAsync(Stock);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // POST: StocksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromBody] CreateStockInputModel model)
        {
            try
            {
                var Stock = _mapper.Map<Stocks>(model);
                await _StockRepository.UpdateStockAsync(Stock);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StocksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StocksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
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
