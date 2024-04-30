using DotNetTestProject.Data;
using DotNetTestProject.Models;
using DotNetTestProject.Models.ViewModels;
using DotNetTestProject.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotNetTestProject.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        
        return View(objProductList);
    }
    public IActionResult Upsert(int? id)
    {
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
            .Select(u=> new SelectListItem {Text = u.Name, Value = u.Id.ToString()});

        // ViewBag.CategoryList = CategoryList;
        // ViewData["CategoryList"] = CategoryList;
        ProductViewModel productViewModel = new ProductViewModel
        {
            CategoryList = CategoryList,
            Product = new Product()
        };
        if (id == null || id == 0)
        {
            return View(productViewModel);
        }

        productViewModel.Product = _unitOfWork.Product.Get(u => u.Id == id);
        return View(productViewModel);
    }
    [HttpPost]
    public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
    {
        
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images/product");

                if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productViewModel.Product.ImageUrl = @"/images/product/" + fileName;
            }
            
            if (productViewModel.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productViewModel.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productViewModel.Product);
            }
            
            _unitOfWork.Save();
            if (productViewModel.Product.Id == 0)
            {
                TempData["success"] = "Product created successfully";
            }
            else
            {
                TempData["success"] = "Product updated successfully";
            }
            
            return RedirectToAction("Index", "Product");
        }
        else
        {
            productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
        };
        return View(productViewModel);
    }
    // public IActionResult Delete(int id)
    // {
    //     if (id == null || id == 0)
    //     {
    //         return NotFound();
    //     }
    //
    //     Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
    //     
    //     if (productFromDb == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(productFromDb);
    // }
    // [HttpPost, ActionName("Delete")]
    // public IActionResult DeletePOST(int? id)
    // {
    //     Product obj = _unitOfWork.Product.Get(u => u.Id == id);
    //
    //     if (obj != null)
    //     {
    //         _unitOfWork.Product.Remove(obj);
    //         _unitOfWork.Save();
    //         TempData["success"] = "Product deleted successfully";
    //         return RedirectToAction("Index", "Product");
    //     }
    //     return View();
    // }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = objProductList });
    }
    
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);

        if (productToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        
        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('/'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        
        _unitOfWork.Product.Remove(productToBeDeleted);
        _unitOfWork.Save();
        
        return Json(new { success = true, message = "Deleted successful" });
    }
    #endregion
}