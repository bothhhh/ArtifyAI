using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtifyAI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAIImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AIImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prompt = table.Column<string>(type: "TEXT", nullable: false),
                    ImageGenerator = table.Column<string>(type: "TEXT", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Filename = table.Column<string>(type: "TEXT", nullable: false),
                    Likes = table.Column<int>(type: "INTEGER", nullable: false),
                    canIncreaseLike = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIImages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIImages");
        }
    }
}
