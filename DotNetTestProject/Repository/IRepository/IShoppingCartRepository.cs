using DotNetTestProject.Models;
using DotNetTestProject.Models.ViewModels;

namespace DotNetTestProject.Repository.IRepository;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    void Update(ShoppingCart obj);
}