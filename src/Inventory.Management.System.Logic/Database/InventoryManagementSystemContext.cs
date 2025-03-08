using System;
using System.Collections.Generic;
using Inventory.Management.System.Logic.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;

namespace Inventory.Management.System.Logic.Database;

public partial class InventoryManagementSystemContext : DbContext
{
    public InventoryManagementSystemContext()
    {
    }

    public InventoryManagementSystemContext(DbContextOptions<InventoryManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<InventoryMovement> InventoryMovements { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<StockAddition> StockAdditions { get; set; }

    public virtual DbSet<StockWithdrawal> StockWithdrawals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Check if any database provider is already registered
            var hasProvider = optionsBuilder.Options.Extensions
                .Any(e => e is RelationalOptionsExtension || e is InMemoryOptionsExtension);

            if (!hasProvider)
            {
                optionsBuilder.UseSqlServer("Server=NAUTILUS\\MSSQLSERVER01;Database=InventoryManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<InventoryMovement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC2791E9C6E1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateMoved)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MovementType).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.InventoryMovements)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Inventory__Produ__5812160E");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Products").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ReorderLevel).HasDefaultValue(10);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<StockAddition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockAdd__3214EC2758E59AD8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.StockAdditions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__StockAddi__Produ__4CA06362");
        });

        modelBuilder.Entity<StockWithdrawal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StockWit__3214EC27A13C332B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateWithdrawn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.StockWithdrawals)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__StockWith__Produ__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
