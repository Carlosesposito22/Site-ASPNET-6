using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using site.Models;
using site.Repositories.Interfaces;
using site.ViewModels;

namespace site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepositoy _lancheRepositoy;

        public HomeController(ILancheRepositoy lancheRepositoy)
        {
            _lancheRepositoy = lancheRepositoy;
        }

        public IActionResult Index()
        {
            var homeVM = new HomeViewModel()
            {
                LanchesPreferidos = _lancheRepositoy.LanchesPreferidos
            };

            return View(homeVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
