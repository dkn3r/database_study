using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lb1.Models
{
    public class Category
    {
        public int CategoryID { get; set; } // Primary key

        [Required]
        [StringLength(50)] // Максимальна довжина назви категорії
        public string CategoryName { get; set; }

        public string Description { get; set; }

        // Навігаційна властивість: Category може мати багато Ads
        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}
