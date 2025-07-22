using Microsoft.EntityFrameworkCore;
using site.Context;

namespace site.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;
        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<Item> Itens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdiconarAoCarrinho(Lanche lanche)
        {
            var item = _context.Itens.SingleOrDefault(
                x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

            if (item == null)
            {
                item = new Item()
                {
                    Quantidade = 1,
                    Lanche = lanche,
                    CarrinhoCompraId = CarrinhoCompraId
                };
                _context.Itens.Add(item);
            }
            else item.Quantidade++;

            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var item = _context.Itens.SingleOrDefault(
                x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

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

        public List<Item> GetCarrinhoCompraItens()
        {
            return Itens ?? (Itens = _context.Itens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId).Include(y => y.Lanche).ToList());
        }

        public void LimparCarrinhoCompra()
        {
            var carrinhoItens = _context.Itens.Where(item => item.CarrinhoCompraId == CarrinhoCompraId);

            _context.Itens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetValorCarrinhoCompra()
        {
            return _context.Itens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId).Select(y => y.Lanche.Preco * y.Quantidade).Sum();
        }
    }
}
