using DotNetTestProject.Data;
using DotNetTestProject.Repository.IRepository;

namespace DotNetTestProject.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext _db;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
    }
    

    public void Save()
    {
        _db.SaveChanges();
    }
}