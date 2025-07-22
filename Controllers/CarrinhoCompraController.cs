using Microsoft.AspNetCore.Mvc;
using site.Models;
using site.Repositories.Interfaces;
using site.ViewModels;

namespace site.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepositoy _lancheRepositoy;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepositoy lancheRepositoy, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepositoy = lancheRepositoy;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.Itens = itens;

            var carrinhoVM = new CarrinhoCompraViewModel()
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetValorCarrinhoCompra()
            };

            return View(carrinhoVM);
        }

        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepositoy.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdiconarAoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepositoy.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}
