using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Wishlist.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Wish> Wishes { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

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
        }
    }
}