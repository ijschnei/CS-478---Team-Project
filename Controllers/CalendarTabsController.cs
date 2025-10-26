using Microsoft.AspNetCore.Mvc;

namespace CS478_EventPlannerProject.Controllers
{
    public class CalendarTabsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
