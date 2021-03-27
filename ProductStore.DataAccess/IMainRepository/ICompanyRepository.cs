using ProductStore.Models.DbModels;

namespace ProductStore.DataAccess.IMainRepository
{
    public interface ICompanyRepository :IRepository<Company>
    {
        void Update(Company company);
    }
}
