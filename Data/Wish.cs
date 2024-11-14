using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wishlist.Data
{
    public class Wish
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        
        public virtual ApplicationUser User { get; set; }

        // New property for Image URL
        public string? ImageUrl { get; set; }

        // New property for Price
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        // New property for Currency
        [Required]
        public string Currency { get; set; }
    }
}