using Microsoft.AspNetCore.Mvc;
using ConfyCreationsSalesSystem.Models;
using ConfyCreationsSalesSystem.Services;

namespace ConfyCreationsSalesSystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISalesService _salesService;

        public SalesController(IProductService productService, ISalesService salesService)
        {
            _productService = productService;
            _salesService = salesService;
        }

        // GET: Sales/Record (Record Sale Form)
        public IActionResult Record()
        {
            ViewBag.Products = _productService.GetAllProducts();
            return View();
        }

        // POST: Sales/Record (Record Sale)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Record(Sale sale)
        {
            var product = _productService.GetProductByProductId(sale.ProductId);
            if (product == null)
            {
                ModelState.AddModelError("ProductId", "Product not found.");
            }

            if (ModelState.IsValid)
            {
                _salesService.RecordSale(sale);
                TempData["SuccessMessage"] = $"Sale recorded successfully! Total: ${product.Price * sale.Quantity:F2}";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Products = _productService.GetAllProducts();
            return View(sale);
        }

        // GET: Sales (View All Sales)
        public IActionResult Index()
        {
            var sales = _salesService.GetAllSales();
            return View(sales);
        }

        // GET: Sales/Daily (View Daily Sales Total)
        public IActionResult Daily()
        {
            var summary = _salesService.GetTodaySalesSummary();
            return View(summary);
        }

        // GET: Sales/History (Sales History - Optional Enhancement)
        public IActionResult History()
        {
            var sales = _salesService.GetAllSales();
            return View(sales);
        }
    }
}