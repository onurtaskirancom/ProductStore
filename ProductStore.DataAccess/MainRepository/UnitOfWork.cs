using ProductStore.Data;
using ProductStore.DataAccess.IMainRepository;

namespace ProductStore.DataAccess.MainRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            sp_call = new SPCallRepository(_db);
        }

        public ICategoryRepository category { get; private set; }

        public ISPCallRepository sp_call { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
