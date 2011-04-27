using System.Web.Mvc;

namespace SocialFreeks.Controllers
{
    [HandleError]
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
