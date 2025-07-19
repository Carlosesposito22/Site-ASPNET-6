using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O nome do lanche deve ser informado")]
        [Display(Name = "Nome do lanche")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "Nome fora do escopo mínimo e máximo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descricao do lanche deve ser informada")]
        [Display(Name = "Descricao do lanche")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Descricao fora do escopo mínimo e máximo")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descricao detalhada do lanche deve ser informada")]
        [Display(Name = "Descricao detalhada do lanche")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Descricao detalhada fora do escopo mínimo e máximo")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preco do lanche")]
        [Display(Name = "Preco")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preco deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da Imagem Normal")]
        [StringLength(200, ErrorMessage = "O texto esta longo demais")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho da Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O texto esta longo demais")]
        public string ImagemThumbnaillUrl { get; set; }

        [Display(Name = "Preferido")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
