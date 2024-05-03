using DotNetTestProject.Repository.IRepository;
using DotNetTestProject.Data;
using DotNetTestProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTestProject.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(AppDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
        _db.Products.Include(u => u.Category);
    }
    
    public void Add(T entity)
    {
        dbSet.Add(entity);
    }
    public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {
        IQueryable<T> query = dbSet;
        
        if (tracked = true)
        {
            query = dbSet;
        }
        else
        { 
            query = dbSet.AsNoTracking();
        }
        
        
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.FirstOrDefault(); 
    }
    public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.ToList();
    }
    public void Remove(T entity) 
    {
        dbSet.Remove(entity);
    }
    public void RemoveRange(IEnumerable<T> entity) 
    {
        dbSet.RemoveRange(entity);
    }
}
