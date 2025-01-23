using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    public partial class PozycjaFakturyJednostkaMiary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JednostkaMiaryId",
                table: "PozycjaFaktury",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_JednostkaMiaryId",
                table: "PozycjaFaktury",
                column: "JednostkaMiaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PozycjaFaktury_JednostkaMiary_JednostkaMiaryId",
                table: "PozycjaFaktury",
                column: "JednostkaMiaryId",
                principalTable: "JednostkaMiary",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PozycjaFaktury_JednostkaMiary_JednostkaMiaryId",
                table: "PozycjaFaktury");

            migrationBuilder.DropIndex(
                name: "IX_PozycjaFaktury_JednostkaMiaryId",
                table: "PozycjaFaktury");

            migrationBuilder.DropColumn(
                name: "JednostkaMiaryId",
                table: "PozycjaFaktury");
        }
    }
}
