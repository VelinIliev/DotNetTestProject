using DotNetTestProject.Models;

namespace DotNetTestProject.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product obj);
}