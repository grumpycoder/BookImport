using BookImport.Models;
using System.Data.Entity;

namespace BookImport.Data
{
    public partial class BeholderContext : DbContext
    {
        public BeholderContext()
            : base("name=BeholderContext")
        {
        }

        public virtual DbSet<ConfidentialityType> ConfidentialityType { get; set; }
        public virtual DbSet<MediaPublished> MediaPublished { get; set; }
        public virtual DbSet<MediaPublishedContext> MediaPublishedContext { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<MovementClass> MovementClass { get; set; }
        public virtual DbSet<PublishedType> PublishedType { get; set; }
        public virtual DbSet<MimeType> MimeType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MimeType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ConfidentialityType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.RenewalPermission)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.County)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.RemovalReason)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublished>()
                .Property(e => e.CatalogId)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublishedContext>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<MediaPublishedContext>()
                .Property(e => e.DocumentExtension)
                .IsUnicode(false);

            modelBuilder.Entity<MediaType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MovementClass>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PublishedType>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}