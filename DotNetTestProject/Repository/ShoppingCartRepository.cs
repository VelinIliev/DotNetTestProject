using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Models.ViewModels;
using DotNetTestProject.Repository.IRepository;

namespace DotNetTestProject.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>,  IShoppingCartRepository
{
    private AppDbContext _db;

    public ShoppingCartRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    

    public void Update(ShoppingCart obj)
    {
        _db.ShoppingCarts.Update(obj);
    }
}