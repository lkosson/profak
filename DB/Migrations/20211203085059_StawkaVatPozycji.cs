using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class StawkaVatPozycji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaFaktury_Towar_TowarId",
                table: "PozycjaFaktury");

            migrationBuilder.AddColumn<int>(
                name: "StawkaVatId",
                table: "PozycjaFaktury",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_StawkaVatId",
                table: "PozycjaFaktury",
                column: "StawkaVatId");

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaFaktury_StawkaVat_StawkaVatId",
                table: "PozycjaFaktury",
                column: "StawkaVatId",
                principalTable: "StawkaVat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaFaktury_Towar_TowarId",
                table: "PozycjaFaktury",
                column: "TowarId",
                principalTable: "Towar",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaFaktury_StawkaVat_StawkaVatId",
                table: "PozycjaFaktury");

            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaFaktury_Towar_TowarId",
                table: "PozycjaFaktury");

            migrationBuilder.DropIndex(
                name: "IX_PozycjaFaktury_StawkaVatId",
                table: "PozycjaFaktury");

            migrationBuilder.DropColumn(
                name: "StawkaVatId",
                table: "PozycjaFaktury");

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaFaktury_Towar_TowarId",
                table: "PozycjaFaktury",
                column: "TowarId",
                principalTable: "Towar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
