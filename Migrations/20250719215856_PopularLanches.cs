using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace site.Migrations
{
    public partial class PopularLanches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Lanches (CategoriaId, DescricaoCurta, DescricaoDetalhada, EmEstoque, " +
                                 "ImagemThumbnaillUrl, ImagemUrl, IsLanchePreferido, Nome, Preco) " +
                                 "VALUES (1, 'Clássico pão francês com queijo.', 'Pão francês fresquinho e queijo prato derretido.', 1, " +
                                 "'pao_queijo_normal_thumb.jpg', 'pao_queijo_normal.jpg', 0, 'Pão com Queijo', 6.50)");

            migrationBuilder.Sql("INSERT INTO Lanches (CategoriaId, DescricaoCurta, DescricaoDetalhada, EmEstoque, " +
                                 "ImagemThumbnaillUrl, ImagemUrl, IsLanchePreferido, Nome, Preco) " +
                                 "VALUES (1, 'Doce fatia de bolo de cenoura com cobertura de chocolate.', 'Bolo fofinho de cenoura com cobertura cremosa de brigadeiro.', 1, " +
                                 "'bolo_cenoura_normal_thumb.jpg', 'bolo_cenoura_normal.jpg', 0, 'Bolo de Cenoura', 9.00)");

            migrationBuilder.Sql("INSERT INTO Lanches (CategoriaId, DescricaoCurta, DescricaoDetalhada, EmEstoque, " +
                     "ImagemThumbnaillUrl, ImagemUrl, IsLanchePreferido, Nome, Preco) " +
                     "VALUES (2, 'Saudável sanduíche de frango no pão integral.', 'Pão integral, peito de frango desfiado, alface, tomate e maionese light.', 1, " +
                     "'sanduiche_frango_integral_thumb.jpg', 'sanduiche_frango_integral.jpg', 1, 'Sanduíche de Frango Integral', 12.00)");

            migrationBuilder.Sql("INSERT INTO Lanches (CategoriaId, DescricaoCurta, DescricaoDetalhada, EmEstoque, " +
                                 "ImagemThumbnaillUrl, ImagemUrl, IsLanchePreferido, Nome, Preco) " +
                                 "VALUES (2, 'Leve e saboroso, perfeito para o café.', 'Pão de forma integral tostado com pasta de ricota e orégano.', 1, " +
                                 "'pao_ricota_integral_thumb.jpg', 'pao_ricota_integral.jpg', 0, 'Pão de Forma Integral com Ricota', 7.50)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
