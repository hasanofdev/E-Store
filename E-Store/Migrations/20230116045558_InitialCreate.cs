using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "IsAdmin", "Name", "Password", "Surname", "UserName" },
                values: new object[,]
                {
                    { 1, true, "Elshad", "Hasanoff17", "Hasanov", "hasanoff2005" },
                    { 2, false, "StepIt", "12345", "Academy", "Step" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, "https://cdn.shopify.com/s/files/1/0206/9470/products/74132_-Apple_Imperfect-Fuji-1.5Kg-Square-_1200x1200px_medium.jpg?v=1649650428", 5.6799999999999997, "Red Apple" },
                    { 2, "https://cdn.shopify.com/s/files/1/0206/9470/products/24141-done_medium.jpg?v=1640211252", 0.5, "Apricot" },
                    { 3, "https://cdn.shopify.com/s/files/1/0206/9470/products/4152-done_medium.jpg?v=1611027746", 2.5, "Avacado" },
                    { 4, "https://cdn.shopify.com/s/files/1/0206/9470/products/4211_-Bananas-Each-Square-_1200x1200px_medium.jpg?v=1650600692", 0.69999999999999996, "Banana" },
                    { 5, "https://cdn.shopify.com/s/files/1/0206/9470/products/4242-done_medium.jpg?v=1625704287", 3.4900000000000002, "Black Berry" },
                    { 6, "https://cdn.shopify.com/s/files/1/0206/9470/products/blueberries-resized_medium.jpg?v=1594262022", 3.9900000000000002, "Blue Berry" },
                    { 7, "https://cdn.shopify.com/s/files/1/0206/9470/products/coconuts_4382_resized_d9dfdbc5-5037-41cb-88d2-43088d2c449c_medium.jpeg?v=1594264018", 5.4900000000000002, "Coconut" },
                    { 9, "https://cdn.shopify.com/s/files/1/0206/9470/products/Black_Grapes_2cdd8a19-e047-43ed-b3ea-d809b0422cfb_medium.jpeg?v=1607485081", 20.289999999999999, "Black Grape" },
                    { 10, "https://cdn.shopify.com/s/files/1/0206/9470/products/grapes_red_seedless_resized_0d6c120f-f276-41e8-8efb-c4d3764be8db_medium.jpeg?v=1594265102", 14.289999999999999, "Red Grape" },
                    { 11, "https://cdn.shopify.com/s/files/1/0206/9470/products/44911-thomson-white-grapes-done_medium.jpg?v=1614895413", 13.289999999999999, "White Grape" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
