using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Repository.IRepository;

namespace DotNetTestProject.Repository;

public class OrderDetailRepository : Repository<OrderDetail>,  IOrderDetailRepository
{
    private AppDbContext _db;

    public OrderDetailRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    

    public void Update(OrderDetail obj)
    {
        _db.OrderDetails.Update(obj);
    }
}