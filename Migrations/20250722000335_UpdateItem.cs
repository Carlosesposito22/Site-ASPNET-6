using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace site.Migrations
{
    public partial class UpdateItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "Itens",
                newName: "CarrinhoCompraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarrinhoCompraId",
                table: "Itens",
                newName: "PedidoId");
        }
    }
}
