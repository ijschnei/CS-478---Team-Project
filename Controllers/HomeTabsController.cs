using Microsoft.AspNetCore.Mvc;

namespace CS478_EventPlannerProject.Controllers
{
    public class HomeTabsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
