using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.EFCore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("SqlServer:MemoryOptimized", true);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcceptedAnswerId = table.Column<int>(nullable: true),
                    AnswerCount = table.Column<int>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    CommentCount = table.Column<int>(nullable: true),
                    CommunityOwnedDate = table.Column<DateTime>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    FavoriteCount = table.Column<int>(nullable: true),
                    LastActivityDate = table.Column<DateTime>(nullable: false),
                    LastEditDate = table.Column<DateTime>(nullable: true),
                    LastEditorDisplayName = table.Column<string>(nullable: true),
                    LastEditorUserId = table.Column<int>(nullable: true),
                    OwnerUserId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    PostTypeId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ViewCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                })
                .Annotation("SqlServer:MemoryOptimized", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Posts")
                .Annotation("SqlServer:MemoryOptimized", true);

            migrationBuilder.AlterDatabase()
                .OldAnnotation("SqlServer:MemoryOptimized", true);
        }
    }
}
