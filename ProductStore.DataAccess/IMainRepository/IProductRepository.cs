using ProductStore.Models.DbModels;

namespace ProductStore.DataAccess.IMainRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
