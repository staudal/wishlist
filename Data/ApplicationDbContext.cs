using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Wishlist.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Wish> Wishes { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ShareLink> ShareLinks { get; set; } // Add this line

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
    }
}