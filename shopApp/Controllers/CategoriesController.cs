using Microsoft.AspNetCore.Mvc;
using shopApp.Models;

namespace shopApp.Controllers;

public class CategoriesController : Controller
{
    private ShopContext _context;

    public CategoriesController(ShopContext context)
    {
        _context = context;
    }
    
    
    public IActionResult Index()
    {
        List<Category> categories = _context.Categories.ToList();
        return View(categories);
    }
    
    public IActionResult Create()
    {
        
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            if (_context.Categories.Any(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Категория с таким именем уже существует");
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(category);
    }
    
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Category category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Category category = _context.Categories.Find(id);
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}