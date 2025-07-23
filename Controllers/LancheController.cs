using Microsoft.AspNetCore.Mvc;
using site.Models;
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

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.Nome);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria)).OrderBy(l => l.Nome);

                categoriaAtual = categoria;
            }

            var lancheViewModel = new LancheListViewModel()
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lancheViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

            return View(lanche);
        }
    }
}
