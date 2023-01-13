using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ThumbnailImagePath = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Excerpt = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 65536, nullable: false),
                    PublishDate = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Published = table.Column<bool>(type: "INTEGER", nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "CategoryId", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title" },
                values: new object[,]
                {
                    { 1, "John Doe", 1, "", "This is the excerpt for post 1. An little ...", "13.01.2023 12:34", true, "uploads/placeholder.jpg", "First post" },
                    { 2, "John Doe", 2, "", "This is the excerpt for post 2. An little ...", "13.01.2023 12:34", true, "uploads/placeholder.jpg", "Second post" },
                    { 3, "John Doe", 3, "", "This is the excerpt for post 3. An little ...", "13.01.2023 12:34", true, "uploads/placeholder.jpg", "Third post" },
                    { 4, "John Doe", 1, "", "This is the excerpt for post 4. An little ...", "13.01.2023 12:34", true, "uploads/placeholder.jpg", "Fourth post" },
                    { 5, "John Doe", 2, "", "This is the excerpt for post 5. An little ...", "13.01.2023 12:34", true, "uploads/placeholder.jpg", "Fifth post" },
                    { 6, "John Doe", 3, "", "This is the excerpt for post 6. An little ...", "13.01.2023 12:34", true, "uploads/placeholder.jpg", "Sixth post" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
