using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopApp.Models;

namespace shopApp.Controllers;

public class ProductsController : Controller
{
    private ShopContext _context;

    public ProductsController(ShopContext context)
    {
        _context = context;
    }
    
    
   public IActionResult Index()
{
    var vm = new ProductOrderViewModel
    {
        Products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .ToList(),
        Orders = _context.Orders
            .Include(o => o.Product)
            .ToList()
    };

    return View(vm);
}
    
    public IActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            product.Category = null;
            product.Brand = null;

            product.CreatedDate = DateTime.Now;
            product.UpdateDate = DateTime.Now;

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        return View(product);
    }




    
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Product product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Product product = _context.Products.Find(id);
        _context.Products.Remove(product);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Product? product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Brands = _context.Brands.ToList();

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.BrandId = product.BrandId;
            existingProduct.UpdateDate = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        return View(product);
    }

    
    public IActionResult Details(int id)
    {
        var product = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefault(p => p.Id == id);

        if (product == null) return NotFound();

        return View(product);
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}