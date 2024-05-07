using System.Security.Claims;
using DotNetTestProject.Models.ViewModels;
using DotNetTestProject.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace DotNetTestProject.ViewComponents;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        if (claim != null)
        {
            if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
            {
                var userId = claim.Value;
                HttpContext.Session.SetInt32(SD.SessionCart, 
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }

            int x = (int)HttpContext.Session.GetInt32(SD.SessionCart);
            
            return View(x);;
        }
        else
        {
            HttpContext.Session.Clear();
            return View(0);
        }
    }
}