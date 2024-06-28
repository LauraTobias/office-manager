using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeManager.Infra.Migrations
{
    public partial class Updating_client_table_delete_behavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Office_OfficeId",
                table: "Client");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Office_OfficeId",
                table: "Client",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Office_OfficeId",
                table: "Client");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Office_OfficeId",
                table: "Client",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
