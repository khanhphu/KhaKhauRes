using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhaKhau.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository; 

        }
        public async Task<IActionResult> Index(string sTerm="")
        {
            var stock=await _stockRepository.GetStocks(sTerm);
            return View(stock);
        }
        public async Task<IActionResult> ManangeStock(int productId)
        {
            var existingStock = await _stockRepository.GetStockByProductId(productId);
            var stock = new StockDTO
            {
                ProductId = productId ,
                Quantity = existingStock != null
                ? existingStock.Quantity : 0
            };
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManangeStock(StockDTO stock)
        {
            if (!ModelState.IsValid)
                return View(stock);
            try
            {
                await _stockRepository.ManageStock(stock);
                TempData["successMessage"] = "Cập nhật món thành công!";
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Đã xảy ra lỗi!!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
