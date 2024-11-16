using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wishlist.Data
{
    public class ShareLink
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Token { get; set; }

        [Required]
        public Guid WishlistId { get; set; }

        [ForeignKey("WishlistId")]
        public virtual Wishlist Wishlist { get; set; }

        public DateTime ExpiryDate { get; set; }
    
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}