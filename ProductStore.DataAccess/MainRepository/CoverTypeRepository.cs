using ProductStore.Data;
using ProductStore.DataAccess.IMainRepository;
using ProductStore.Models.DbModels;
using System.Linq;

namespace ProductStore.DataAccess.MainRepository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            var data = _db.CoverTypes.FirstOrDefault(c => c.Id == coverType.Id);
            if (data != null)
            {
                data.Name = coverType.Name;
            }
        }
    }
}
