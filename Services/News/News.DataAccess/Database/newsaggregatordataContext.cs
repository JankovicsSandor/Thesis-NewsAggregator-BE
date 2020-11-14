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
                    .HasColumnType("longtext");

                entity.Property(e => e.FeedId)
                    .HasColumnName("feedId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasColumnType("longtext");

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasColumnType("longtext");

                entity.Property(e => e.PublishDate)
                    .HasColumnName("publishDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("longtext");

                entity.Property(e => e.Guid)
                   .HasColumnName("guid")
                   .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Feed>(entity =>
            {
                entity.ToTable("feed");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasColumnType("longtext");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
