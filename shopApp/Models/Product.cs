using System.ComponentModel.DataAnnotations;

namespace shopApp.Models;

public class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите название товара")]
    public string Name { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
    public int Price { get; set; }
    public DateTime CreatedDate{ get; set; } =  DateTime.Now;
    public DateTime UpdateDate { get; set; } =  DateTime.Now;
    
    [Required(ErrorMessage = "Выберите категорию")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    [Required(ErrorMessage = "Выберите бренд")]
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
    
    public string? ImageUrl { get; set; }
    
}

