using Microsoft.AspNetCore.Mvc;

namespace CS478_EventPlannerProject.Controllers
{
    public class EventsTabsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
