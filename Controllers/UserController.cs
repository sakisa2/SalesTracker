using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesTracker.Data;
using SalesTracker.Models;

namespace SalesTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Action for adding a user
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(User user)
        {
            Console.WriteLine("=== CreateUser POST called ===");
            Console.WriteLine($"Username: {user.Username}, Role: {user.Role}, IsActive: {user.IsActive}");

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState is valid, adding user...");
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                Console.WriteLine("User saved in the database.");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState is NOT valid:");
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($" - {kv.Key}: {err.ErrorMessage}");

            return View(user);
        }

        // Action for listing users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // Action for deactivating a user
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = false;  
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
