using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wishlist.Data;

public class ShareLink
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public int WishlistId { get; set; }

    [ForeignKey("WishlistId")]
    public virtual Wishlist Wishlist { get; set; }

    public DateTime ExpiryDate { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}