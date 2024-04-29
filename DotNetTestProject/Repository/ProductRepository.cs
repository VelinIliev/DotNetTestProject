using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Repository.IRepository;

namespace DotNetTestProject.Repository;

public class ProductRepository : Repository<Product>,  IProductRepository
{
    private AppDbContext _db;

    public ProductRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    

    public void Update(Product obj)
    {
        _db.Products.Update(obj);
    }
}