using Microsoft.AspNetCore.Mvc;

namespace CS478_EventPlannerProject.Controllers
{
    public class AboutTabsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
