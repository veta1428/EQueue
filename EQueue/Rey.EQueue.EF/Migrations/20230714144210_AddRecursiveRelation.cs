using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rey.EQueue.EF.Migrations
{
    public partial class AddRecursiveRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Records_NextRecordId",
                table: "Records",
                column: "NextRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Records_NextRecordId",
                table: "Records",
                column: "NextRecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Records_NextRecordId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_NextRecordId",
                table: "Records");
        }
    }
}
