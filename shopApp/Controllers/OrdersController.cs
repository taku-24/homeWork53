using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopApp.Models;

namespace shopApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShopContext _context;

        public OrdersController(ShopContext context)
        {
            _context = context;
        }

        // список заказов
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Product)
                .ToList();
            return View(orders);
        }

        // при нажатии "Заказать"
        public IActionResult Create(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return NotFound();

            var order = new Order
            {
                ProductId = product.Id,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // удаление заказа
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}