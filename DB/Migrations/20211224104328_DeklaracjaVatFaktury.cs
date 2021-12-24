using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class DeklaracjaVatFaktury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeklaracjaVatId",
                table: "Faktura",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_DeklaracjaVatId",
                table: "Faktura",
                column: "DeklaracjaVatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faktura_DeklaracjaVat_DeklaracjaVatId",
                table: "Faktura",
                column: "DeklaracjaVatId",
                principalTable: "DeklaracjaVat",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faktura_DeklaracjaVat_DeklaracjaVatId",
                table: "Faktura");

            migrationBuilder.DropIndex(
                name: "IX_Faktura_DeklaracjaVatId",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "DeklaracjaVatId",
                table: "Faktura");
        }
    }
}
