using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetTestProject.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Category Name")]
    [MaxLength(30, ErrorMessage = "Name max length is 30 symbols"), 
     MinLength(3, ErrorMessage = "Name must be at least 3 symbols")]
    public string Name { get; set; }
    [DisplayName("Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
    public int DisplayOrder { get; set; }
} 