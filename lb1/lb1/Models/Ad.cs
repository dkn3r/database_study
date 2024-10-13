using System;
using System.Collections.Generic;
namespace lb1.Models;
public class Ad
{
    public int AdID { get; set; } // Primary key
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Foreign key до User
    public int UserID { get; set; }
    public User User { get; set; } // Навігаційна властивість

    // Foreign key до Category
    public int CategoryID { get; set; }
    public Category Category { get; set; } // Навігаційна властивість

    // Навігаційна властивість: Ad може мати багато Messages
    public ICollection<Message> Messages { get; set; }
}
