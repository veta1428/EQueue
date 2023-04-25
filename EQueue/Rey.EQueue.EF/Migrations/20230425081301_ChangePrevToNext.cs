using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rey.EQueue.EF.Migrations
{
    public partial class ChangePrevToNext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrevRecordId",
                table: "Records",
                newName: "NextRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextRecordId",
                table: "Records",
                newName: "PrevRecordId");
        }
    }
}
