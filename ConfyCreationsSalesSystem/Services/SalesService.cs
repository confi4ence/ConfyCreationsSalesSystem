using ConfyCreationsSalesSystem.Models;
using System.Collections.Concurrent;

namespace ConfyCreationsSalesSystem.Services
{
    public class SalesService : ISalesService
    {
        private readonly ConcurrentDictionary<int, Sale> _sales = new();
        private int _nextId = 1;
        private readonly IProductService _productService;

        public SalesService(IProductService productService)
        {
            _productService = productService;
        }

        public List<Sale> GetAllSales()
        {
            return _sales.Values.OrderByDescending(s => s.SaleDate).ToList();
        }

        public Sale GetSaleById(int id)
        {
            return _sales.TryGetValue(id, out var sale) ? sale : null;
        }

        public void RecordSale(Sale sale)
        {
            var product = _productService.GetProductByProductId(sale.ProductId);
            if (product != null)
            {
                sale.Id = _nextId++;
                sale.ProductName = product.Name;
                sale.UnitPrice = product.Price;
                sale.TotalAmount = product.Price * sale.Quantity;
                _sales[sale.Id] = sale;
            }
        }

        public SalesSummary GetDailySalesSummary(DateTime date)
        {
            var dailySales = _sales.Values
                .Where(s => s.SaleDate.Date == date.Date)
                .OrderByDescending(s => s.SaleDate)
                .ToList();

            return new SalesSummary
            {
                Date = date.Date,
                TotalSalesCount = dailySales.Count,
                TotalRevenue = dailySales.Sum(s => s.TotalAmount),
                Sales = dailySales
            };
        }

        public SalesSummary GetTodaySalesSummary()
        {
            return GetDailySalesSummary(DateTime.Today);
        }

        public void DeleteSale(int id)
        {
            _sales.TryRemove(id, out _);
        }
    }
}
