using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace AgriEnergyConnect.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Unauthorized();

                var farmer = _context.Farmers.FirstOrDefault(f => f.Email == user.Email);
                if (farmer == null)
                {
                    ModelState.AddModelError("", "No matching Farmer profile found.");
                    return View(product);
                }

                product.FarmerId = farmer.Id;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction(nameof(MyProducts));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding the product.";
                return View(product);
            }
        }

        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> MyProducts()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Unauthorized();

                var farmer = _context.Farmers.FirstOrDefault(f => f.Email == user.Email);
                if (farmer == null) return NotFound("Farmer profile not found.");

                var products = _context.Products
                    .Where(p => p.FarmerId == farmer.Id)
                    .ToList();

                return View(products);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load your products.";
                return View(Enumerable.Empty<Product>());
            }
        }

        [Authorize(Roles = "Employee")]
        public IActionResult ViewAll()
        {
            try
            {
                return View(new ProductFilterViewModel
                {
                    Categories = _context.Products.Select(p => p.Category).Distinct().ToList(),
                    Products = _context.Products.ToList()
                });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error loading product list.";
                return View(new ProductFilterViewModel { Categories = new List<string>(), Products = new List<Product>() });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public IActionResult ViewAll(ProductFilterViewModel filter)
        {
            try
            {
                var products = _context.Products.AsQueryable();

                if (filter.StartDate.HasValue && filter.EndDate.HasValue)
                {
                    products = products.Where(p => p.ProductionDate >= filter.StartDate.Value &&
                                                   p.ProductionDate <= filter.EndDate.Value);
                }

                if (!string.IsNullOrEmpty(filter.SelectedCategory))
                {
                    products = products.Where(p => p.Category == filter.SelectedCategory);
                }

                filter.Categories = _context.Products.Select(p => p.Category).Distinct().ToList();
                filter.Products = products.ToList();

                return View(filter);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error filtering products.";
                filter.Categories = new List<string>();
                filter.Products = new List<Product>();
                return View(filter);
            }
        }
    }
}
