using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Models.ViewModels;
using DotNetTestProject.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Utility;

namespace DotNetTestProject.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class UserController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _db;
    
    // public UserController(IUnitOfWork unitOfWork)
    // {
    //     _unitOfWork = unitOfWork;
    // }
    public UserController(AppDbContext db, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _db = db;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
        
        return View(objCompanyList);
    }

    public IActionResult RoleManagement(string userId)
    {
        string RoleID = _db.UserRoles.FirstOrDefault(u => u.UserId == userId).RoleId;

        RoleManagementViewModel roleVM = new RoleManagementViewModel()
        {
            ApplicationUser = _db.ApplicationUsers.Include(u => u.Company).FirstOrDefault(u=> u.Id == userId),
            RoleList = _db.Roles.Select( i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Name
            }),
            CompanyList = _db.Companies.Select( i=> new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };
        roleVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(u => u.Id == RoleID).Name;
            
        return View(roleVM);
    }
    
    [HttpPost]
    public IActionResult RoleManagement(RoleManagementViewModel roleVM)
    {
        string RoleID = _db.UserRoles.FirstOrDefault(u => u.UserId == roleVM.ApplicationUser.Id).RoleId;
        string oldRole = _db.Roles.FirstOrDefault(u => u.Id == RoleID).Name;

        if (!(roleVM.ApplicationUser.Role == oldRole))
        {
            ApplicationUser applicationUser =
                _db.ApplicationUsers.FirstOrDefault(u => u.Id == roleVM.ApplicationUser.Id);
            if (roleVM.ApplicationUser.Role == SD.Role_Company)
            {
                applicationUser.CompanyId = roleVM.ApplicationUser.CompanyId;
            }

            if (oldRole == SD.Role_Company)
            {
                applicationUser.CompanyId = null;
            }

            _db.SaveChanges();

            _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(applicationUser, roleVM.ApplicationUser.Role).GetAwaiter().GetResult();
        }
            
        return RedirectToAction("Index");
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<ApplicationUser> objUserList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();

        var userRoles = _db.UserRoles.ToList();
        var roles = _db.Roles.ToList();
        
        foreach (var user in objUserList)
        {
            var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
            user.Role = _db.Roles.FirstOrDefault(u => u.Id == roleId).Name;
            
            if (user.Company == null)
            {
                user.Company = new() {Name= ""};
            }
        }
        return Json(new { data = objUserList });
    }
    
    [HttpPost]
    public IActionResult LockUnlock([FromBody]string id)
    {
        var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);

        if (objFromDb == null)
        {
            Json(new { success = true, message = "Error while Locking / Unlocking" });
        }

        if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.UtcNow)
        {
            objFromDb.LockoutEnd = DateTime.UtcNow;
        }
        else
        {
            objFromDb.LockoutEnd = DateTime.UtcNow.AddYears(1);
        }

        _db.SaveChanges();
        
        return Json(new { success = true, message = "Locked / Unlocked successfully" });
    }
    #endregion
}