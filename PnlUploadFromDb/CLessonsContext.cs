using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PnlUploadFromDb
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

            modelBuilder.Entity<PnlInsert>(entity =>
            {
                entity.HasKey(e => e.Date)
                    .HasName("PK__pnl_inse__77387D06D02D53E5");

                entity.ToTable("pnl_insert");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Amount).HasColumnType("numeric(38, 8)");

                entity.Property(e => e.StrategyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
