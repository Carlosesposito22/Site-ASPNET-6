using Microsoft.AspNetCore.Mvc;

namespace site.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
