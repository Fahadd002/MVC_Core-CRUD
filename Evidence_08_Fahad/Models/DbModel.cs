using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Evidence_08_Fahad.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(50)]
        public string ProductName { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime? ExpireDate { get; set; }
        [Required, StringLength(40)]
        public string Picture { get; set; } = default!;
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
    public class Sale
    {
        public int SaleId { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? SaleDate { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; } = default!;
    }
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Walnut", Price = 250, ExpireDate = DateTime.Parse("2024-05-01"), Picture = "1.jpg" },
                 new Product { ProductId = 2, ProductName = "Pecans", Price = 250, ExpireDate = DateTime.Parse("2024-05-01"), Picture = "2.jpg" }
                );
            modelBuilder.Entity<Sale>().HasData(
                new Sale { SaleId = 1, SaleDate = DateTime.Parse("2024-02-23"), Quantity = 3, ProductId = 1 },
                new Sale { SaleId = 2, SaleDate = DateTime.Parse("2024-02-23"), Quantity = 4, ProductId = 2 }
                );
        }

    }
}
