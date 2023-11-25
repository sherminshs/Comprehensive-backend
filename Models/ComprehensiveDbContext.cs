using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Comprehensive_backend.Models;

public partial class ComprehensiveDbContext : DbContext
{
    public ComprehensiveDbContext()
    {
    }

    public ComprehensiveDbContext(DbContextOptions<ComprehensiveDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Related> Relateds { get; set; }

    public virtual DbSet<Retired> Retireds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-A590VBD;Database=ComprehensiveDB;User ID=sa;Password=shermin;Trusted_Connection=True;trustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Related>(entity =>
        {
            entity.ToTable("Related");

            entity.Property(e => e.RelatedId)
                .ValueGeneratedNever()
                .HasColumnName("RelatedID");
            entity.Property(e => e.RelatedFirstName).HasMaxLength(100);
            entity.Property(e => e.RelatedLastName).HasMaxLength(100);
            entity.Property(e => e.RetiredId).HasColumnName("RetiredID");

            entity.HasOne(d => d.Retired).WithMany(p => p.Relateds)
                .HasForeignKey(d => d.RetiredId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Related_Retired");
        });

        modelBuilder.Entity<Retired>(entity =>
        {
            entity.ToTable("Retired");

            entity.Property(e => e.RetiredId)
                .ValueGeneratedNever()
                .HasColumnName("RetiredID");
            entity.Property(e => e.RetiredFirstName).HasMaxLength(100);
            entity.Property(e => e.RetiredLastName).HasMaxLength(100);
            entity.Property(e => e.RetiredNationalCode)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
