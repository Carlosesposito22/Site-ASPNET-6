using Microsoft.AspNetCore.Mvc;
using site.Repositories.Interfaces;

namespace site.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepositoy _lancheRepository;

        public LancheController(ILancheRepositoy lancheRepositoy)
        {
            _lancheRepository = lancheRepositoy;
        }

        public IActionResult List()
        {
            var lanches = _lancheRepository.Lanches;
            return View(lanches);
        }
    }
}
