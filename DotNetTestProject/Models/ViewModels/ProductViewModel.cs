using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetTestProject.Models.ViewModels;

public class ProductViewModel
{
    public Product Product { set; get; }
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList {set; get; }
}