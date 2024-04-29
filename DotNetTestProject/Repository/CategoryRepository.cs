using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Repository.IRepository;

namespace DotNetTestProject.Repository;

public class CategoryRepository : Repository<Category>,  ICategoryRepository
{
    private AppDbContext _db;

    public CategoryRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }
}