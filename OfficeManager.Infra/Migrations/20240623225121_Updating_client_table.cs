using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeManager.Infra.Migrations
{
    public partial class Updating_client_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OfficeId",
                table: "Client",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Client_OfficeId",
                table: "Client",
                column: "OfficeId");

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

            migrationBuilder.DropIndex(
                name: "IX_Client_OfficeId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Client");
        }
    }
}
