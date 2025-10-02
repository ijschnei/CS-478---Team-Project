using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly UserManager<Users> _userManager;
        public DashboardController(IDashboardService dashboardService, UserManager<Users> userManager)
        {
            _dashboardService = dashboardService;
            _userManager = userManager;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = currentUser?.Id;
                var viewModel = new DashboardViewModel
                {
                    TotalEvents = await _dashboardService.GetTotalEventsCountAsync(userId),
                    UpcomingEvents = await _dashboardService.GetUpcomingEventsCountAsync(userId),
                    TotalAttendees = await _dashboardService.GetTotalAttendeesCountAsync(userId),
                    RecentEvents = await _dashboardService.GetRecentEventsAsync(userId, 5),
                    EventsByStatus = await _dashboardService.GetEventsByStatusAsync(userId),
                    EventsByCategory = await _dashboardService.GetEventsByCategoryAsync(userId)
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load dashboard data.";
                return View(new DashboardViewModel());
            }
        }

        // GET: Dashboard/Admin (Admin-only dashboard
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            try
            {
                var viewModel = new AdminDashboardViewModel
                {
                    TotalEvents = await _dashboardService.GetTotalEventsCountAsync(),
                    UpcomingEvents = await _dashboardService.GetUpcomingEventsCountAsync(),
                    TotalAttendees = await _dashboardService.GetTotalAttendeesCountAsync(),
                    RecentEvents = await _dashboardService.GetRecentEventsAsync(null, 10),
                    PopularEvents = await _dashboardService.GetPopularEventsAsync(10),
                    EventsByStatus = await _dashboardService.GetEventsByStatusAsync(),
                    EventsByCategory = await _dashboardService.GetEventsByCategoryAsync()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to load admin dashboard data.";
                return View(new AdminDashboardViewModel());
            }
        }

        // GET: Dashboard/GetEventsByStatus (AJAX endpoint)
        [HttpGet]
        public async Task<IActionResult> GetEventsByStatus()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = User.IsInRole("Admin") ? null : currentUser?.Id;

                var eventsByStatus = await _dashboardService.GetEventsByStatusAsync(userId);
                return Json(eventsByStatus);
            }
            catch (Exception)
            {
                return Json(new Dictionary<string, int>());
            }
        }

        // GET: Dashboard/GetEventsByCategory (AJAX endpoint)
        [HttpGet]
        public async Task<IActionResult> GetEventsByCategory()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = User.IsInRole("Admin") ? null : currentUser?.Id;

                var eventsByCategory = await _dashboardService.GetEventsByCategoryAsync(userId);
                return Json(eventsByCategory);
            }
            catch (Exception)
            {
                return Json(new Dictionary<string, int>());
            }
        }

        // GET: Dashboard/GetPopularEvents (AJAX endpoint)
        [HttpGet]
        public async Task<IActionResult> GetPopularEvents(int count = 5)
        {
            try
            {
                var popularEvents = await _dashboardService.GetPopularEventsAsync(count);
                var result = popularEvents.Select(e => new
                {
                    id = e.EventId,
                    name = e.EventName,
                    attendeeCount = e.AttendeeCount,
                    startDateTime = e.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                    location = e.FormattedLocation,
                    creator = e.Creator?.Profile?.FullName ?? "Unknown"
                });
                return Json(result);
            }
            catch (Exception)
            {
                return Json(new List<object>());
            }
        }
       
    }
    //View Models for Dashboard
    public class DashboardViewModel
    {
        public int TotalEvents { get; set; }
        public int UpcomingEvents { get; set; }
        public int TotalAttendees { get; set; }
        public IEnumerable<Events> RecentEvents { get; set; } = new List<Events>();
        public Dictionary<string, int> EventsByStatus { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> EventsByCategory { get; set; } = new Dictionary<string, int>();
    }
    public class AdminDashboardViewModel : DashboardViewModel
    {
        public IEnumerable<Events> PopularEvents { get; set; } = new List<Events>();
    }
}
