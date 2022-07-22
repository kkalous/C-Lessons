using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PnlUploadFromDb.Scaffolded
{
    public partial class CLessonsContext : DbContext
    {
        public CLessonsContext()
        {
        }

        public CLessonsContext(DbContextOptions<CLessonsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PnlCapital> PnlCapitals { get; set; }
        public virtual DbSet<Strategy> Strategies { get; set; }

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

            modelBuilder.Entity<PnlCapital>(entity =>
            {
                entity.HasKey(e => new { e.StrategyId, e.Date, e.AmountType })
                    .HasName("PK__PnlCapit__5C21CC104D84AE9F");

                entity.ToTable("PnlCapital");

                entity.Property(e => e.StrategyId).HasColumnType("numeric(38, 0)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.AmountType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("numeric(38, 8)");

                entity.HasOne(d => d.Strategy)
                    .WithMany(p => p.PnlCapitals)
                    .HasForeignKey(d => d.StrategyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PnlCapita__Strat__73BA3083");
            });

            modelBuilder.Entity<Strategy>(entity =>
            {
                entity.ToTable("strategy");

                entity.Property(e => e.StrategyId)
                    .HasColumnType("numeric(38, 0)")
                    .ValueGeneratedOnAdd();

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
