using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieLibrary.Data.Migrations
{
    public partial class update_rate_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieRate");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Rate",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Rate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rate_MovieId",
                table: "Rate",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Rate",
                table: "Rate",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Rate",
                table: "Rate");

            migrationBuilder.DropIndex(
                name: "IX_Rate_MovieId",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Rate");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Rate",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "MovieRate",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRate", x => new { x.MovieId, x.Id });
                    table.ForeignKey(
                        name: "FK_MovieRates_Movies",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieID");
                    table.ForeignKey(
                        name: "FK_MovieRates_Rates",
                        column: x => x.Id,
                        principalTable: "Rate",
                        principalColumn: "RateId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieRate_Id",
                table: "MovieRate",
                column: "Id");
        }
    }
}
