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
    }
}