using Microsoft.AspNetCore.Mvc;
using ConfyCreationsSalesSystem.Models;
using ConfyCreationsSalesSystem.Services;

namespace ConfyCreationsSalesSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products (View All Products)
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        // GET: Products/Create (Add Product Form)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create (Add Product)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (_productService.ProductExists(product.ProductId))
                {
                    ModelState.AddModelError("ProductId", "Product ID already exists.");
                    return View(product);
                }

                _productService.AddProduct(product);
                TempData["SuccessMessage"] = $"Product '{product.Name}' added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                TempData["SuccessMessage"] = $"Product '{product.Name}' updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                _productService.DeleteProduct(id);
                TempData["SuccessMessage"] = $"Product '{product.Name}' deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
