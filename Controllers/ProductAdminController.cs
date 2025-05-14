using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Data;
using System;
using System.Linq;

namespace AgriEnergyConnect.Controllers
{
    public class ProductAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Shared access (if you ever want to make this public later)
        public IActionResult Index()
        {
            var allProducts = _context.Products.ToList();
            return View(allProducts);
        }

        // Restricted: Only Employees can use the filtering feature
        [Authorize(Roles = "Employee")]
        public IActionResult Filter(string category, DateTime? fromDate, DateTime? toDate)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);

            if (fromDate.HasValue)
                products = products.Where(p => p.ProductionDate >= fromDate.Value);

            if (toDate.HasValue)
                products = products.Where(p => p.ProductionDate <= toDate.Value);

            return View(products.ToList());
        }
    }
}
