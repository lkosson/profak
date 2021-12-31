using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class ZaliczkaPitFaktury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZaliczkaPitId",
                table: "Faktura",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_ZaliczkaPitId",
                table: "Faktura",
                column: "ZaliczkaPitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faktura_ZaliczkaPit_ZaliczkaPitId",
                table: "Faktura",
                column: "ZaliczkaPitId",
                principalTable: "ZaliczkaPit",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faktura_ZaliczkaPit_ZaliczkaPitId",
                table: "Faktura");

            migrationBuilder.DropIndex(
                name: "IX_Faktura_ZaliczkaPitId",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "ZaliczkaPitId",
                table: "Faktura");
        }
    }
}
