using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rey.EQueue.EF.Migrations
{
    public partial class RefactorChangeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFromFirstName",
                table: "ChangeRequest");

            migrationBuilder.DropColumn(
                name: "UserFromLastName",
                table: "ChangeRequest");

            migrationBuilder.DropColumn(
                name: "UserToFirstName",
                table: "ChangeRequest");

            migrationBuilder.DropColumn(
                name: "UserToLastName",
                table: "ChangeRequest");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequest_QueueId",
                table: "ChangeRequest",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequest_UserFromId",
                table: "ChangeRequest",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRequest_UserToId",
                table: "ChangeRequest",
                column: "UserToId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequest_Queues_QueueId",
                table: "ChangeRequest",
                column: "QueueId",
                principalTable: "Queues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequest_Users_UserFromId",
                table: "ChangeRequest",
                column: "UserFromId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRequest_Users_UserToId",
                table: "ChangeRequest",
                column: "UserToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRequest_Queues_QueueId",
                table: "ChangeRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRequest_Users_UserFromId",
                table: "ChangeRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRequest_Users_UserToId",
                table: "ChangeRequest");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRequest_QueueId",
                table: "ChangeRequest");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRequest_UserFromId",
                table: "ChangeRequest");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRequest_UserToId",
                table: "ChangeRequest");

            migrationBuilder.AddColumn<string>(
                name: "UserFromFirstName",
                table: "ChangeRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserFromLastName",
                table: "ChangeRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserToFirstName",
                table: "ChangeRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserToLastName",
                table: "ChangeRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
