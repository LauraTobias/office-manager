using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeManager.Infra.Migrations
{
    public partial class Updating_tables_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConclusioDate",
                table: "Case",
                newName: "ConclusionDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConclusionDate",
                table: "Case",
                newName: "ConclusioDate");
        }
    }
}
