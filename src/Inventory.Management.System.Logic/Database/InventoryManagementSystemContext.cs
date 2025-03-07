using System;
using System.Collections.Generic;
using Inventory.Management.System.Logic.Database.Models;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NAUTILUS\\MSSQLSERVER01;Database=InventoryManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Products").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
