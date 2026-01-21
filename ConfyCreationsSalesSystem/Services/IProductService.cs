using ConfyCreationsSalesSystem.Models;

namespace ConfyCreationsSalesSystem.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByProductId(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        bool ProductExists(int productId);
    }
}
