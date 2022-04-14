using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;

namespace MarketingBox.TrackingLink.Service.Postgres
{
    public class DatabaseContext : MyDbContext
    {
        public const string Schema = "trackinglink-service";
        public const string TrackingLinkTable = "trackinglinks";

        public DbSet<Domain.Models.TrackingLink> TrackingLinks { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (LoggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(LoggerFactory).EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            
            modelBuilder.Entity<Domain.Models.TrackingLink>().ToTable(TrackingLinkTable);
            
            modelBuilder.Entity<Domain.Models.TrackingLink>().HasKey(x => x.Id);
            
            modelBuilder.Entity<Domain.Models.TrackingLink>().OwnsOne(x => x.LinkParameterValues);
            modelBuilder.Entity<Domain.Models.TrackingLink>().OwnsOne(x => x.LinkParameterNames);
            modelBuilder.Entity<Domain.Models.TrackingLink>().Property(x => x.Link).IsRequired();
            modelBuilder.Entity<Domain.Models.TrackingLink>().Property(x => x.BrandId).IsRequired();
            modelBuilder.Entity<Domain.Models.TrackingLink>().Property(x => x.AffiliateId).IsRequired();
            modelBuilder.Entity<Domain.Models.TrackingLink>().Property(x => x.UniqueId).IsRequired();
            
            modelBuilder.Entity<Domain.Models.TrackingLink>()
                .Property(x => x.ClickId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Domain.Models.TrackingLink>().HasIndex(x => x.ClickId).IsUnique();
            modelBuilder.Entity<Domain.Models.TrackingLink>().HasIndex(x => x.UniqueId);
        }
    }
}
