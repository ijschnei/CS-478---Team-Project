using CS478_EventPlannerProject.Models;
using Microsoft.AspNetCore.Mvc;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CS478_EventPlannerProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get upcoming active events (next 4 events)
            var upcomingEvents = await _context.Events
                .Where(e => e.StartDateTime >= DateTime.Now && !e.IsPrivate && e.IsActive && !e.IsDeleted)
                .OrderBy(e => e.StartDateTime)
                .Take(4)
                .ToListAsync();

            return View(upcomingEvents);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

}
