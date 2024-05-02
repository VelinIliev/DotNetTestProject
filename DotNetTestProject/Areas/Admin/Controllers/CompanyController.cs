using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Models.ViewModels;
using DotNetTestProject.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace DotNetTestProject.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
        
        return View(objCompanyList);
    }
    public IActionResult Upsert(int? id)
    {
        if (id == null || id == 0)
        {
            return View(new Company());
        }

        Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
        return View(companyObj);
    }
    [HttpPost]
    public IActionResult Upsert(Company companyObj)
    {
        
        if (ModelState.IsValid)
        {
            
            if (companyObj.Id == 0)
            {
                _unitOfWork.Company.Add(companyObj);
            }
            else
            {
                _unitOfWork.Company.Update(companyObj);
            }
            
            _unitOfWork.Save();
            if (companyObj.Id == 0)
            {
                TempData["success"] = "Company created successfully";
            }
            else
            {
                TempData["success"] = "Company updated successfully";
            }
            
            return RedirectToAction("Index", "Company");
        }
        
        return View(companyObj);
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
        return Json(new { data = objCompanyList });
    }
    
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);

        if (companyToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        
        _unitOfWork.Company.Remove(companyToBeDeleted);
        _unitOfWork.Save();
        
        return Json(new { success = true, message = "Deleted successful" });
    }
    #endregion
}