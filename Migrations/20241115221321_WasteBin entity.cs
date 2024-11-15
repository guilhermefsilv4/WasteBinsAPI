using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteBinsAPI.Migrations
{
    /// <inheritdoc />
    public partial class WasteBinentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WasteBins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Location = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Capacity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FillLevel = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteBins", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WasteBins");
        }
    }
}
