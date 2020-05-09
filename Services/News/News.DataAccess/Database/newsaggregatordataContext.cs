using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace News.DataAccess.Database
{
    public partial class newsaggregatordataContext : DbContext
    {
        public newsaggregatordataContext()
        {
        }

        public newsaggregatordataContext(DbContextOptions<newsaggregatordataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Feed> Feed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3308;user=admin;password=Genius1?;database=newsaggregatordata", x => x.ServerVersion("5.7.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_hungarian_ci");

                entity.Property(e => e.FeedId)
                    .HasColumnName("feedId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_hungarian_ci");

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_hungarian_ci");

                entity.Property(e => e.PublishDate)
                    .HasColumnName("publishDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_hungarian_ci");
            });

            modelBuilder.Entity<Feed>(entity =>
            {
                entity.ToTable("feed");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_hungarian_ci");

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_hungarian_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
