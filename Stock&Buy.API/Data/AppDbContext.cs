using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Stock_Buy.API.Domain;
using System;

namespace Stock_Buy.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bundle>(entity =>
            {
                entity.HasMany(entity => entity.AssociatedBundles)
               .WithMany()
               .UsingEntity<AssociatedBundle>(
                   l => l.HasOne(x => x.ChildBundle).WithMany().HasForeignKey(x => x.ChildBundleId),
                   r => r.HasOne(x => x.ParentBundle).WithMany().HasForeignKey(x => x.ParentBundleId),
                   j => j.HasKey(x => new { x.ParentBundleId, x.ChildBundleId })

                   );

                entity.HasMany(entity => entity.AssociatedParts)
                .WithMany(entity => entity.Bundles)
                .UsingEntity<AssociatedPart>(
                    l => l.HasOne(x => x.Part).WithMany().HasForeignKey(x => x.PartId),
                    r => r.HasOne(x => x.Bundle).WithMany().HasForeignKey(x => x.BundleId),
                    j => j.HasKey(x => new { x.BundleId, x.PartId })
                    );

                entity.HasKey(x => x.Id);
                entity.ToTable("Bundles");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Parts");
            });

            modelBuilder.Entity<AssociatedPart>(entity =>
            {
                entity.HasOne(entity => entity.Bundle);
                entity.ToTable("AssociatedParts");
            });

            modelBuilder.Entity<AssociatedBundle>(entity =>
            {
                entity.ToTable("AssociatedBundles");
            });
        }
        public DbSet<Bundle> Bundles { get; init; }
        public DbSet<Part> Parts { get; init; }
        public DbSet<AssociatedBundle> AssociatedBundles { get; init; }
        public DbSet<AssociatedPart> AssociatedParts { get; init; }
    }
}
