using Microsoft.EntityFrameworkCore;
using site.Context;
using site.Models;

namespace site.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = _context.Pedidos.AsQueryable();

            if (minDate.HasValue)
                result = result.Where(x => x.PedidoEnviado >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(x => x.PedidoEnviado <= maxDate.Value);

            return await result
                .Include(p => p.PedidoItens)
                .ThenInclude(p => p.Lanche)
                .OrderByDescending(p => p.PedidoEnviado)
                .ToListAsync();
        }
    }
}
