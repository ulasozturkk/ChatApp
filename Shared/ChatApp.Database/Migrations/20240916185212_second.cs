using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_chatRooms_ChatRoomID",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "users");

            migrationBuilder.RenameIndex(
                name: "IX_User_ChatRoomID",
                table: "users",
                newName: "IX_users_ChatRoomID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_users_chatRooms_ChatRoomID",
                table: "users",
                column: "ChatRoomID",
                principalTable: "chatRooms",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_chatRooms_ChatRoomID",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_users_ChatRoomID",
                table: "User",
                newName: "IX_User_ChatRoomID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_chatRooms_ChatRoomID",
                table: "User",
                column: "ChatRoomID",
                principalTable: "chatRooms",
                principalColumn: "ID");
        }
    }
}
