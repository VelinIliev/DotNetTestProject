using DotNetTestProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace DotNetTestProject.Data.DbInitializer;

public class DbInitializer : IDbInitializer
{
    public readonly UserManager<IdentityUser> _userManager;
    public readonly RoleManager<IdentityRole> _roleManager;
    public readonly AppDbContext _db;

    public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
    }
    
    public void Initialize()
    {
        
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0 )
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            
        }
        
        if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@abv.bg",
                Email = "admin@abv.bg",
                Name = "Velin Iliev",
                PhoneNumber = "0123456789",
                StreetAddress = "Street Ave",
                State = "SF",
                PostalCode = "12345",
                City = "Sofia"
            }, "Admin123!").GetAwaiter().GetResult();
        
            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@abv.bg");

            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
        return;
    }
}