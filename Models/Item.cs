using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
    [Table("Itens")]
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public int LancheId { get; set; }
        public virtual Lanche Lanche { get; set; }

        [StringLength(200)]
        public string CarrinhoCompraId { get; set; }
    }
}
