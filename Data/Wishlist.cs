using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wishlist.Data
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Wish> Wishes { get; set; } = new List<Wish>();

        public DateTime CreationDate { get; set; } = DateTime.UtcNow; // Add this line
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow; // Add this line

        // Computed property (not stored in database)
        [NotMapped]
        public int ItemsCount => Wishes.Count; // This line calculates the number of items
        
        public bool IsShared { get; set; }
    }
}