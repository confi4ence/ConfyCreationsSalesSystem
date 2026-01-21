using ConfyCreationsSalesSystem.Models;
using System.Collections.Concurrent;

namespace ConfyCreationsSalesSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ConcurrentDictionary<int, Product> _products = new();
        private int _nextId = 1;

        public ProductService()
        {
            // Add some sample products
            AddSampleProducts();
        }

        private void AddSampleProducts()
        {
            var sampleProducts = new List<Product>
            {
                new Product { ProductId = 101, Name = "Custom Mug", Price = 15.99m },
                new Product { ProductId = 102, Name = "Personalized T-Shirt", Price = 24.99m },
                new Product { ProductId = 103, Name = "Business Card Holder", Price = 12.50m },
                new Product { ProductId = 104, Name = "Desk Name Plate", Price = 18.75m },
                new Product { ProductId = 105, Name = "Branded Pen Set", Price = 9.99m }
            };

            foreach (var product in sampleProducts)
            {
                product.Id = _nextId++;
                _products[product.Id] = product;
            }
        }

        public List<Product> GetAllProducts()
        {
            return _products.Values.OrderBy(p => p.ProductId).ToList();
        }

        public Product GetProductById(int id)
        {
            return _products.TryGetValue(id, out var product) ? product : null;
        }

        public Product GetProductByProductId(int productId)
        {
            return _products.Values.FirstOrDefault(p => p.ProductId == productId);
        }

        public void AddProduct(Product product)
        {
            product.Id = _nextId++;
            _products[product.Id] = product;
        }

        public void UpdateProduct(Product product)
        {
            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;
            }
        }

        public void DeleteProduct(int id)
        {
            _products.TryRemove(id, out _);
        }

        public bool ProductExists(int productId)
        {
            return _products.Values.Any(p => p.ProductId == productId);
        }
    }
}