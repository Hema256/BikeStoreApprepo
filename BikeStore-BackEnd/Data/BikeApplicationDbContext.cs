using BikeStore_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStore_BackEnd.Data
{
    public class BikeApplicationDbContext : DbContext
    {
        public BikeApplicationDbContext(DbContextOptions<BikeApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Review> Reviews { get; set; }  // Adding DbSet for Reviews

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing relationship configurations...

            // Order and OrderItem relationship (One-to-Many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // Product and OrderItem relationship (One-to-Many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // ShoppingCart and User relationship (One-to-Many)
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.ShoppingCartItems)
                .HasForeignKey(sc => sc.UserId);

            // ShoppingCart and Product relationship (One-to-Many)
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Product)
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(sc => sc.ProductId);

            // Review and Product relationship (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)  // Bike refers to Product
                .WithMany(p => p.Reviews)  // A Product can have many reviews
                .HasForeignKey(r => r.BikeId);

            // Review and User relationship (One-to-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)  // A User can have many reviews
                .HasForeignKey(r => r.UserId);

            // Additional configurations...

            // Unique index on User Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Add other constraints, indexes, or default values as required
        }
    }
}
