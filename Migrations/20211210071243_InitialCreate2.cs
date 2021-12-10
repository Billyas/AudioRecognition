using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioRecognition.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "FlashRecognitionResults",
                columns: table => new
                {
                    Request_id = table.Column<string>(nullable: false),
                    Audio_duration = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Flash_result = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashRecognitionResults", x => x.Request_id);
                    table.ForeignKey(
                        name: "FK_FlashRecognitionResults_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiveRecognitionResults",
                columns: table => new
                {
                    Voice_id = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    MessageId = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveRecognitionResults", x => x.Voice_id);
                    table.ForeignKey(
                        name: "FK_LiveRecognitionResults_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShortRecognitionResults",
                columns: table => new
                {
                    RequestId = table.Column<string>(nullable: false),
                    AudioDuration = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortRecognitionResults", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_ShortRecognitionResults_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashRecognitionResults_Username",
                table: "FlashRecognitionResults",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_LiveRecognitionResults_Username",
                table: "LiveRecognitionResults",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_ShortRecognitionResults_Username",
                table: "ShortRecognitionResults",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashRecognitionResults");

            migrationBuilder.DropTable(
                name: "LiveRecognitionResults");

            migrationBuilder.DropTable(
                name: "ShortRecognitionResults");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
