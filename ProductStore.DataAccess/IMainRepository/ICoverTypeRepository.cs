using ProductStore.Models.DbModels;

namespace ProductStore.DataAccess.IMainRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType category);
    }
}
