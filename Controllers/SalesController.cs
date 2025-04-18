using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesTracker.Data;
using SalesTracker.Models;

namespace SalesTracker.Controllers
{
    public class SaleController : Controller
    {
        private readonly AppDbContext _context;

        public SaleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Sale/
        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales.ToListAsync();
            return View(sales);
        }

        // GET: /Sale/CreateSale
        [HttpGet]
        public IActionResult CreateSale()
        {
            return View();
        }

        // POST: /Sale/CreateSale
        [HttpPost]
        public async Task<IActionResult> CreateSale(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sale);
        }

        // GET: /Sale/EditSale/5
        [HttpGet]
        public async Task<IActionResult> EditSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
                return NotFound();

            if ((DateTime.Now - sale.Date).TotalHours > 24)
            {
                TempData["Error"] = "Ne možete izmeniti prodaju jer je prošlo više od 24h od unosa.";
                return RedirectToAction(nameof(Index));
            }

            return View(sale);
        }

        // POST: /Sale/EditSale/5
        [HttpPost]
        public async Task<IActionResult> EditSale(int id, Sale updatedSale)
        {
            if (id != updatedSale.Id)
                return NotFound();

            var existingSale = await _context.Sales.FindAsync(id);
            if (existingSale == null)
                return NotFound();

            if ((DateTime.Now - existingSale.Date).TotalHours > 24)
            {
                TempData["Error"] = "Ne možete izmeniti prodaju jer je prošlo više od 24h od unosa.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                existingSale.Amount = updatedSale.Amount;
                existingSale.Description = updatedSale.Description;
                existingSale.Date = updatedSale.Date;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Prodaja je uspešno izmenjena!";
                return RedirectToAction(nameof(Index));
            }

            return View(updatedSale);
        }
    }
}
