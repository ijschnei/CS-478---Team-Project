using Microsoft.AspNetCore.Mvc;

namespace CS478_EventPlannerProject.Controllers
{
    public class ContactsTabsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
