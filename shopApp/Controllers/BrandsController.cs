using Microsoft.AspNetCore.Mvc;
using shopApp.Models;

namespace shopApp.Controllers;

public class BrandsController : Controller
{
    private ShopContext _context;

    public BrandsController(ShopContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        List<Brand> brands = _context.Brands.ToList();
        return View(brands);
    }
    
    public IActionResult Create()
    {
        
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Brand brand)
    {
        if (ModelState.IsValid)
        {
            if (_context.Brands.Any(c => c.Name == brand.Name))
            {
                ModelState.AddModelError("Name", "Бренд с таким именем уже существует");
                return View(brand);
            }

            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(brand);
    }
    
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Brand brand = _context.Brands.Find(id);
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Brand brands = _context.Brands.Find(id);
        _context.Brands.Remove(brands);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

}