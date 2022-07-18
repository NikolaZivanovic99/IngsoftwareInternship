using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieLibrary.Data.Migrations
{
    public partial class Addmanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieUser",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser", x => new { x.MovieId, x.Id });
                    table.ForeignKey(
                        name: "FK_MovieUsers_Movies",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieID");
                    table.ForeignKey(
                        name: "FK_MovieUsers_Users",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser_Id",
                table: "MovieUser",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieUser");
        }
    }
}
