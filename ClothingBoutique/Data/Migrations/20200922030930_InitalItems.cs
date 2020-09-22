using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingBoutique.Data.Migrations
{
    public partial class InitalItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemName = table.Column<string>(nullable: true),
                    itemCategory = table.Column<string>(nullable: true),
                    inStock = table.Column<bool>(nullable: false),
                    featured = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");
        }
    }
}
