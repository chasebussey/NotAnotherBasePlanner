using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    public partial class CreatePriceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    MaterialTicker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExchangeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MarketMakerBuy = table.Column<int>(type: "int", nullable: false),
                    MarketMakerSell = table.Column<int>(type: "int", nullable: false),
                    PriceAverage = table.Column<double>(type: "float", nullable: false),
                    Ask = table.Column<double>(type: "float", nullable: false),
                    Supply = table.Column<int>(type: "int", nullable: false),
                    BidCount = table.Column<int>(type: "int", nullable: false),
                    Bid = table.Column<double>(type: "float", nullable: false),
                    Demand = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => new { x.MaterialTicker, x.ExchangeCode });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Price");
        }
    }
}
