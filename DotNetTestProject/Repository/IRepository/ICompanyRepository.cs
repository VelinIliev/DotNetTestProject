using DotNetTestProject.Models;

namespace DotNetTestProject.Repository.IRepository;

public interface ICompanyRepository : IRepository<Company>
{
    void Update(Company obj);
}