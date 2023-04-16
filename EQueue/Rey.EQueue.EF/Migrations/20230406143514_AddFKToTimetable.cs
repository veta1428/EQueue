using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rey.EQueue.EF.Migrations
{
    public partial class AddFKToTimetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectInstances_Timetables_TimetableId",
                table: "SubjectInstances");

            migrationBuilder.DropIndex(
                name: "IX_SubjectInstances_TimetableId",
                table: "SubjectInstances");

            migrationBuilder.DropColumn(
                name: "TimetableId",
                table: "SubjectInstances");

            migrationBuilder.AddColumn<int>(
                name: "SubjectInstanceId",
                table: "Timetables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timetables_SubjectInstanceId",
                table: "Timetables",
                column: "SubjectInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_SubjectInstances_SubjectInstanceId",
                table: "Timetables",
                column: "SubjectInstanceId",
                principalTable: "SubjectInstances",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_SubjectInstances_SubjectInstanceId",
                table: "Timetables");

            migrationBuilder.DropIndex(
                name: "IX_Timetables_SubjectInstanceId",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "SubjectInstanceId",
                table: "Timetables");

            migrationBuilder.AddColumn<int>(
                name: "TimetableId",
                table: "SubjectInstances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInstances_TimetableId",
                table: "SubjectInstances",
                column: "TimetableId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectInstances_Timetables_TimetableId",
                table: "SubjectInstances",
                column: "TimetableId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
