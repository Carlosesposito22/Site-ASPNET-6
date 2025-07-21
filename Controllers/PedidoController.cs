using Microsoft.AspNetCore.Mvc;
using site.Models;
using site.Repositories.Interfaces;
using site.ViewModels;

namespace site.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILancheRepositoy _lancheRepositoy;
        private readonly Pedido _pedido;

        public PedidoController(ILancheRepositoy lancheRepositoy, Pedido pedido)
        {
            _lancheRepositoy = lancheRepositoy;
            _pedido = pedido;
        }

        public IActionResult Index()
        {
            var itens = _pedido.GetItens();
            _pedido.Itens = itens;

            var pedidoVM = new PedidoViewModel()
            {
                Pedido = _pedido,
                PedidoTotal = _pedido.GetValorPedido()
            };

            return View(pedidoVM);
        }

        public RedirectToActionResult AdicionarItemNoPedido(int lancheId)
        {
            var lancheSelecionado = _lancheRepositoy.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _pedido.AdiconarAoPedido(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverItemDoPedido(int lancheId)
        {
            var lancheSelecionado = _lancheRepositoy.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _pedido.RemoverDoPedido(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}
