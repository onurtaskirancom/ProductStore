﻿using ProductStore.Data;
using ProductStore.DataAccess.IMainRepository;
using ProductStore.Models.DbModels;
using System.Linq;

namespace ProductStore.DataAccess.MainRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var data = _db.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (data != null)
            {
                data.CategoryName = category.CategoryName;
            }
        }
    }
}
