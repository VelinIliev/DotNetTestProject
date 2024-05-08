using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetTestProject.Models.ViewModels;

public class RoleManagementViewModel
{
    public ApplicationUser ApplicationUser { get; set; }
    public IEnumerable<SelectListItem> RoleList { get; set; }
    public IEnumerable<SelectListItem> CompanyList { get; set; }
}