using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Wishlist.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Wish> Wishes { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ShareLink> ShareLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Wish>()
                .HasMany(w => w.Wishlists)
                .WithMany(w => w.Wishes)
                .UsingEntity<Dictionary<string, object>>(
                    "WishlistWish",
                    j => j.HasOne<Wishlist>().WithMany().HasForeignKey("WishlistId"),
                    j => j.HasOne<Wish>().WithMany().HasForeignKey("WishId"),
                    j =>
                    {
                        j.HasKey("WishId", "WishlistId");
                        j.HasIndex(new[] { "WishId", "WishlistId" }).IsUnique(); // Ensure combination is unique
                    }
                );

            // Configure the ShareLink entity
            builder.Entity<ShareLink>(entity =>
            {
                entity.HasKey(sl => sl.Id);

                entity.HasIndex(sl => sl.Token).IsUnique(); // Ensure the Token is unique

                entity.Property(sl => sl.Token)
                      .IsRequired()
                      .HasMaxLength(128); // Set a reasonable max length for the token

                entity.Property(sl => sl.ExpiryDate)
                      .IsRequired();

                entity.HasOne(sl => sl.Wishlist)
                      .WithMany()
                      .HasForeignKey(sl => sl.WishlistId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete the share links when the wishlist is deleted
            });
        }

        public override int SaveChanges()
        {
            UpdateWishlistLastUpdated();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateWishlistLastUpdated();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateWishlistLastUpdated()
        {
            var modifiedWishes = ChangeTracker.Entries<Wish>()
                                               .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted)
                                               .ToList();

            foreach (var entry in modifiedWishes)
            {
                var wish = entry.Entity;
                foreach (var wishlist in wish.Wishlists)
                {
                    wishlist.LastUpdated = DateTime.UtcNow;
                }
            }
        }
    }
}