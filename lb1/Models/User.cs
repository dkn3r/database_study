using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lb1.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary key

        [Required]
        [StringLength(50)] // Максимальна довжина імені користувача
        public string Username { get; set; }

        [Required]
        [EmailAddress] // Валідація для електронної пошти
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        // Навігаційна властивість: User може мати багато Ads
        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}
