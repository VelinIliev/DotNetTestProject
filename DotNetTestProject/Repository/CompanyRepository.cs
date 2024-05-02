using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Repository.IRepository;

namespace DotNetTestProject.Repository;

public class CompanyRepository: Repository<Company>,  ICompanyRepository
{
    private AppDbContext _db;

    public CompanyRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    

    public void Update(Company obj)
    {
        _db.Companies.Update(obj);
    }
}