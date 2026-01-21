using ConfyCreationsSalesSystem.Models;

namespace ConfyCreationsSalesSystem.Services
{
    public interface ISalesService
    {
        List<Sale> GetAllSales();
        Sale GetSaleById(int id);
        void RecordSale(Sale sale);
        SalesSummary GetDailySalesSummary(DateTime date);
        SalesSummary GetTodaySalesSummary();
        void DeleteSale(int id);
    }
}
