using E_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Store.Repositories;

internal class SQLiteDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source = EStore.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product(1, "Red Apple", 5.68, "https://cdn.shopify.com/s/files/1/0206/9470/products/74132_-Apple_Imperfect-Fuji-1.5Kg-Square-_1200x1200px_medium.jpg?v=1649650428"),
            new Product(2, "Apricot", 0.5, "https://cdn.shopify.com/s/files/1/0206/9470/products/24141-done_medium.jpg?v=1640211252"),
            new Product(3, "Avacado", 2.5, "https://cdn.shopify.com/s/files/1/0206/9470/products/4152-done_medium.jpg?v=1611027746"),
            new Product(4, "Banana", 0.7, "https://cdn.shopify.com/s/files/1/0206/9470/products/4211_-Bananas-Each-Square-_1200x1200px_medium.jpg?v=1650600692"),
            new Product(5, "Black Berry", 3.49, "https://cdn.shopify.com/s/files/1/0206/9470/products/4242-done_medium.jpg?v=1625704287"),
            new Product(6, "Blue Berry", 3.99, "https://cdn.shopify.com/s/files/1/0206/9470/products/blueberries-resized_medium.jpg?v=1594262022"),
            new Product(7, "Coconut", 5.49, "https://cdn.shopify.com/s/files/1/0206/9470/products/coconuts_4382_resized_d9dfdbc5-5037-41cb-88d2-43088d2c449c_medium.jpeg?v=1594264018"),
            new Product(9, "Black Grape", 20.29, "https://cdn.shopify.com/s/files/1/0206/9470/products/Black_Grapes_2cdd8a19-e047-43ed-b3ea-d809b0422cfb_medium.jpeg?v=1607485081"),
            new Product(10, "Red Grape", 14.29, "https://cdn.shopify.com/s/files/1/0206/9470/products/grapes_red_seedless_resized_0d6c120f-f276-41e8-8efb-c4d3764be8db_medium.jpeg?v=1594265102"),
            new Product(11, "White Grape", 13.29, "https://cdn.shopify.com/s/files/1/0206/9470/products/44911-thomson-white-grapes-done_medium.jpg?v=1614895413")
        );

        modelBuilder.Entity<Member>().HasData(
            new Member(1, "Elshad", "Hasanov", "hasanoff2005", "Hasanoff17", true),
            new Member(2, "StepIt", "Academy", "Step", "12345", false)
        );
    }
}