using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetTestProject.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    public string Description { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public double ListPrice { get; set; }
    
    [Required]
    [DisplayName("Price for 1-50")]
    [Range(0.1, 1000)]
    public double Price {get; set; }
    
    [Required]
    [DisplayName("Price for 50+")]
    [Range(0.1, 1000)]
    public double Price50 {get; set; }
    
    [Required]
    [DisplayName("Price for 100+")]
    [Range(0.1, 1000)]
    public double Price100 {get; set; }
    
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
    
    public string ImageUrl { get; set; }
}