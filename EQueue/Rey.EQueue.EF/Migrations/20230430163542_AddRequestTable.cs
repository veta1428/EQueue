using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rey.EQueue.EF.Migrations
{
    public partial class AddRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordFromId = table.Column<int>(type: "int", nullable: true),
                    RecordToId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubjectInstanceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFromFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFromLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserToFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserToLastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChangeRequestId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeRequest_Records_RecordFromId",
                        column: x => x.RecordFromId,
                        principalTable: "Records",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChangeRequest_Records_RecordToId",
                        column: x => x.RecordToId,
                        principalTable: "Records",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequest_RecordFromId",
                table: "ChangeRequest",
                column: "RecordFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequest_RecordToId",
                table: "ChangeRequest",
                column: "RecordToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeRequest");
        }
    }
}
