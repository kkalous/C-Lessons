using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Scaffolded
{
    public partial class GsaContext : DbContext
    {
        public GsaContext()
        {
        }

        public GsaContext(DbContextOptions<GsaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Capital> Capitals { get; set; }
        public virtual DbSet<Pnl> Pnls { get; set; }
        public virtual DbSet<Strategy> Strategies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Gsa;User ID=sa;Password=kk30101995;");
            }
        }

        public void Seed(IEnumerable<Strategy> strategiesWithPnlAndCapital)
        {
            TruncateTables();
            Strategies.AddRange(strategiesWithPnlAndCapital.ToList());
            this.SaveChanges();
        }

        private void TruncateTables()
        {
            this.Database.ExecuteSqlRaw("TRUNCATE TABLE [Capital]");
            this.Database.ExecuteSqlRaw("TRUNCATE TABLE [Pnl]");
            this.Database.ExecuteSqlRaw("DELETE FROM  [Strategy]");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Capital>(entity =>
            {
                entity.ToTable("Capital");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Strategy)
                    .WithMany(p => p.Capitals)
                    .HasForeignKey(d => d.StrategyId)
                    .HasConstraintName("FK__Capital__Strateg__29572725");
            });

            modelBuilder.Entity<Pnl>(entity =>
            {
                entity.ToTable("Pnl");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Strategy)
                    .WithMany(p => p.Pnls)
                    .HasForeignKey(d => d.StrategyId)
                    .HasConstraintName("FK__Pnl__StrategyId__267ABA7A");
            });

            modelBuilder.Entity<Strategy>(entity =>
            {
                entity.ToTable("Strategy");

                entity.Property(e => e.Region)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StrategyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
