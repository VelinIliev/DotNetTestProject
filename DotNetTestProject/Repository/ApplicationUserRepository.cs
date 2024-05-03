using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Repository.IRepository;

namespace DotNetTestProject.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>,  IApplicationUserRepository
{
    private AppDbContext _db;

    public ApplicationUserRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
}