using DotNetTestProject.Models;

namespace DotNetTestProject.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
}