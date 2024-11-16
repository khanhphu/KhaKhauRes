using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhaKhau.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        // GET: ReportsController
        public async Task<ActionResult> TopFiveSellingProducts(DateTime? sDate = null, DateTime? eDate = null)
        {
            try
            {
                // by default, get last 7 days record
                DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7); //nếu không chọn ngày thì tự trừ đi 7 ngày 
                DateTime endDate = eDate ?? DateTime.UtcNow; //nếu không chọn ngày thì là hôm nay
                var topFiveSellingBooks = await _reportRepository.GetTopSoldProductByDate(startDate, endDate);
                var vm = new TopSoldProductByDate(startDate, endDate, topFiveSellingBooks);
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Xảy ra lỗi vui lòng thử lại!";
                return RedirectToAction("Index", "ProductManager");
            }
        }
    }
}
