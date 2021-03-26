using System;

namespace ProductStore.DataAccess.IMainRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository category { get; }
        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; }
        ISPCallRepository sp_call { get; }

        void Save();
    }
}
