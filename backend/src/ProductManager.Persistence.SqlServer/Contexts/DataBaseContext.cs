using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Models;

namespace ProductManager.Persistence.SqlServer.Contexts;

public class DataBaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProductModel> Products
    {
        get; set;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            entity.Property(e => e.Stock)
                .IsRequired();
        });
    }
}

