using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ResourceConfigurator.DataAccess.Database
{
    public partial class newsaggregatorresourceContext : DbContext
    {
        public newsaggregatorresourceContext()
        {
        }

        public newsaggregatorresourceContext(DbContextOptions<newsaggregatorresourceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lastsynchronizedresource> Lastsynchronizedresource { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lastsynchronizedresource>(entity =>
            {
                entity.ToTable("lastsynchronizedresource");

                entity.HasIndex(e => e.ResourceId)
                    .HasName("FK_RESOURCE_SYNC_ID_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("longtext");

                entity.Property(e => e.ResourceId)
                    .HasColumnName("resourceId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("longtext");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.Lastsynchronizedresource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RESOURCE_SYNC_ID");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("resource");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active");

                entity.Property(e => e.FeedId)
                   .HasColumnName("feedId");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("longtext");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
