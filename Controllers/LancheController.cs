using Microsoft.AspNetCore.Mvc;
using site.Repositories.Interfaces;
using site.ViewModels;

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
            var lancheViewModel = new LancheListViewModel();
            lancheViewModel.Lanches = _lancheRepository.Lanches;
            lancheViewModel.CategoriaAtual = "Categoria Atual";

            return View(lancheViewModel);
        }
    }
}
