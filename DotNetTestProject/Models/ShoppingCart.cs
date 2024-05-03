using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace DotNetTestProject.Models.ViewModels;

public class ShoppingCart
{
    public int Id { set; get; }
    
    public int ProductId { set; get; }
    [ForeignKey("ProductId")]
    [ValidateNever]
    public Product Product { get; set; }
    
    [Range(1, 1000, ErrorMessage = "Please enter value between 1 and 1000")]
    public int Count { set; get; }
    
    public string ApplicationUserId { set; get; }
    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; } 
    
    [NotMapped]
    public double Price { get; set; }
}