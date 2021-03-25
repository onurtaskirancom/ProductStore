using ProductStore.Models.DbModels;

namespace ProductStore.DataAccess.IMainRepository
{
    public interface ICategoryRepository :IRepository<Category>
    {
        void Update(Category category);
    }
}
