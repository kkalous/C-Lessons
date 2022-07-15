using System;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PnlData.Scaffolded
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Pnl> Pnls { get; set; }
        public virtual DbSet<PnlInsert> PnlInserts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=C#-Lessons;User ID=sa;Password=kk30101995;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Pnl>(entity =>
            {
                entity.HasKey(e => e.Date)
                    .HasName("PK__pnl__77387D06FD52DDF1");

                entity.ToTable("pnl");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Strategy1).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy10).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy11).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy12).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy13).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy14).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy15).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy2).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy3).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy4).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy5).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy6).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy7).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy8).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.Strategy9).HasColumnType("numeric(38, 8)");
            });

            modelBuilder.Entity<PnlInsert>(entity =>
            {
                entity.HasKey(e => new {e.Date, e.Amount, e.StrategyName })
                    .HasName("[pnl_insert_pk]");

                entity.ToTable("pnl_insert");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.StrategyName).HasColumnType("varchar(50)");

                entity.Property(e => e.Amount).HasColumnType("numeric(38, 8)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
