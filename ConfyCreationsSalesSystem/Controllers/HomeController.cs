using System.Diagnostics;
using ConfyCreationsSalesSystem.Models;
using Microsoft.AspNetCore.Mvc;
using ConfyCreationsSalesSystem.Services;

namespace ConfyCreationsSalesSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISalesService _salesService;

        public HomeController(IProductService productService, ISalesService salesService)
        {
            _productService = productService;
            _salesService = salesService;
        }

        // GET: Home/Index (Main Menu)
        public IActionResult Index()
        {
            var dashboard = new DashboardViewModel
            {
                TotalProducts = _productService.GetAllProducts().Count,
                TodaySalesSummary = _salesService.GetTodaySalesSummary()
            };
            return View(dashboard);
        }
    }

    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }
        public SalesSummary TodaySalesSummary { get; set; }
    }
}