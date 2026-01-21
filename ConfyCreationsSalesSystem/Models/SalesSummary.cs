namespace ConfyCreationsSalesSystem.Models
{
    public class SalesSummary
    {
        public DateTime Date { get; set; }
        public int TotalSalesCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}