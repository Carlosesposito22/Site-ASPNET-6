using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
    [Table("PedidoDetalhe")]
    public class PedidoDetalhe
    {
        [Key]
        public int PedidoDetalheId { get; set; }

        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }

        public int LancheId { get; set; }
        public virtual Lanche Lanche { get; set; }

        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
    }
}
