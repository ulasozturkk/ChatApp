using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatRooms",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatRooms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatRoomID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SenderUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_messages_chatRooms_ChatRoomID",
                        column: x => x.ChatRoomID,
                        principalTable: "chatRooms",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PushToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatRoomID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_chatRooms_ChatRoomID",
                        column: x => x.ChatRoomID,
                        principalTable: "chatRooms",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_ChatRoomID",
                table: "messages",
                column: "ChatRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_User_ChatRoomID",
                table: "User",
                column: "ChatRoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "chatRooms");
        }
    }
}
