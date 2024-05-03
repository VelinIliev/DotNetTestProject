using DotNetTestProject.Models;

namespace DotNetTestProject.Repository.IRepository;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    void Update(OrderDetail obj);
}