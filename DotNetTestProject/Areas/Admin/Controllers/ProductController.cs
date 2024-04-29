using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotNetTestProject.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
        
        return View(objProductList);
    }
    public IActionResult Create()
    {
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
            .Select(u=> new SelectListItem {Text = u.Name, Value = u.Id.ToString()});

        ViewBag.CategoryList = CategoryList;
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product obj)
    {
 
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index", "Product");
        }
        return View();
    }
    public IActionResult Edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product productFromDb = _unitOfWork.Product.Get(u=>u.Id==id);
        
        if (productFromDb == null)
        {
            return NotFound();
        }
        
        return View(productFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToAction("Index", "Product");
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Product obj = _unitOfWork.Product.Get(u => u.Id == id);

        if (obj != null)
        {
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index", "Product");
        }
        return View();
    }
}