using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using site.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
    public class Pedido
    {
        private readonly AppDbContext _context;
        public Pedido(AppDbContext context)
        {
            _context = context;
        }

        public string PedidoId { get; set; }
        public List<Item> Itens { get; set; }

        public static Pedido GetPedido(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string pedidoId = session.GetString("PedidoId") ?? Guid.NewGuid().ToString();
            session.SetString("PedidoId", pedidoId);

            return new Pedido(context)
            {
                PedidoId = pedidoId
            };
        }

        public void AdiconarAoPedido(Lanche lanche)
        {
            var item = _context.Itens.SingleOrDefault(
                x => x.Lanche.LancheId == lanche.LancheId && x.PedidoId == PedidoId);

            if (item == null)
            {
                item = new Item()
                {
                    Quantidade = 1,
                    Lanche = lanche,
                    PedidoId = PedidoId
                };
                _context.Itens.Add(item);
            }
            else item.Quantidade++;

            _context.SaveChanges();
        }

        public int RemoverDoPedido(Lanche lanche)
        {
            var item = _context.Itens.SingleOrDefault(
                x => x.Lanche.LancheId == lanche.LancheId && x.PedidoId == PedidoId);

            var quantidadeLocal = 0;

            if (item != null)
            {
                if (item.Quantidade > 1)
                {
                    item.Quantidade--;
                    quantidadeLocal = item.Quantidade;
                }
                else
                {
                    _context.Itens.Remove(item);
                }
            }

            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<Item> GetItens()
        {
            return Itens ?? (Itens = _context.Itens.Where(x => x.PedidoId == PedidoId).Include(y => y.Lanche).ToList());
        }

        public void LimparPedido()
        {
            var pedido = _context.Itens.Where(item => item.PedidoId == PedidoId);

            _context.Itens.RemoveRange(pedido);
            _context.SaveChanges();
        }

        public decimal GetValorPedido()
        {
            return _context.Itens.Where(x => x.PedidoId == PedidoId).Select(y => y.Lanche.Preco * y.Quantidade).Sum();
        }
    }
}
