using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wishlist.Data
{
    public class Wish
    {
        [Key] public int Id { get; set; }

        [Required] public string Title { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string? ImageUrl { get; set; }

        [DataType(DataType.Currency)] public decimal Price { get; set; }

        [Required] public string Currency { get; set; }

        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        [Required] public int Amount { get; set; } = 1;

        public string? Description { get; set; }
        
        public string? LinkUrl { get; set; }  // New property
    }
}