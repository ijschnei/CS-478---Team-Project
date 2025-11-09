using Microsoft.AspNetCore.Mvc;
using CS478_EventPlannerProject.Services.Interfaces;
using CS478_EventPlannerProject.Models;

namespace CS478_EventPlannerProject.Controllers
{
    public class CalendarTabsController : Controller
    {
        private readonly IEventService _eventService;

        public CalendarTabsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index(int? year, int? month)
        {
            var currentDate = DateTime.Now;
            var selectedYear = year ?? currentDate.Year;
            var selectedMonth = month ?? currentDate.Month;

            var events = await _eventService.GetAllEventsAsync();

            var monthEvents = events.Where(e =>
                e.StartDateTime.Year == selectedYear &&
                e.StartDateTime.Month == selectedMonth).ToList();

            ViewBag.SelectedYear = selectedYear;
            ViewBag.SelectedMonth = selectedMonth;
            ViewBag.Events = monthEvents;

            return View();
        }
    }
}