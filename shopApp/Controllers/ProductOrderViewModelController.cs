using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopApp.Models;

namespace shopApp.Controllers;

public class ProductOrderViewModelController : Controller
{
    private ShopContext _context;

    public ProductOrderViewModelController(ShopContext context)
    {
        _context = context;
    }
    
    
    public IActionResult Index()
    {
        var vm = new ProductOrderViewModel
        {
            Products = _context.Products.Include(p => p.Category).Include(p => p.Brand).ToList(),
            Orders = _context.Orders.Include(o => o.Product).ToList()
        };

        return View(vm);
    }
}