using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using System.Threading.Tasks;
using System;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Employee")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Farmer farmer)
        {
            if (!ModelState.IsValid)
                return View(farmer);

            try
            {
                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Farmer profile created successfully!";
                return RedirectToAction(nameof(Create));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the farmer profile.";
                return View(farmer);
            }
        }
    }
}
