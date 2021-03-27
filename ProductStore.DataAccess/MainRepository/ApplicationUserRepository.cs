using ProductStore.Data;
using ProductStore.DataAccess.IMainRepository;
using ProductStore.Models.DbModels;

namespace ProductStore.DataAccess.MainRepository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
