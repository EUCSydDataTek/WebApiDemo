using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendData.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PluralsightUrl = table.Column<string>(type: "TEXT", nullable: true),
                    TwitterAlias = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Name", "PluralsightUrl", "TwitterAlias" },
                values: new object[,]
                {
                    { 1, "Martin Fowler", "https://app.pluralsight.com/profile/martin-fowler", "https://twitter.com/martinfawler" },
                    { 2, "Eric Evans", "https://app.pluralsight.com/profile/eric-evans", "https://twitter.com/ericevans" },
                    { 3, "Steve Smith", "https://app.pluralsight.com/profile/steve-smith", "https://twitter.com/stevesmith" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
