using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContactFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserContactId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "UserContactId",
                table: "Contacts",
                newName: "UserSenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_UserContactId",
                table: "Contacts",
                newName: "IX_Contacts_UserSenderId");

            migrationBuilder.AddColumn<int>(
                name: "UserReceiverId",
                table: "Contacts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserSenderId",
                table: "Contacts",
                column: "UserSenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserSenderId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserReceiverId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "UserSenderId",
                table: "Contacts",
                newName: "UserContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_UserSenderId",
                table: "Contacts",
                newName: "IX_Contacts_UserContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserContactId",
                table: "Contacts",
                column: "UserContactId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
