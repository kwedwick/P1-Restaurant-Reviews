using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    /// <summary>
    /// This is what the user inputs and is sent through Domain and Data.Access to store in the database.
    /// </summary>
    public class CreateReview
    { 
        public CreateReview() { }
        public CreateReview(string title, string body, int rating, string userId, int restaurantId) : this()
        {
            Title = title;
            Body = body;
            Rating = rating;
            UserId = userId;
            RestaurantId = restaurantId;
        }
        public CreateReview(int id, string title, string body, int rating, string userId, int restaurantId) : this()
        {
            Id = id;
            Title = title;
            Body = body;
            Rating = rating;
            UserId = userId;
            RestaurantId = restaurantId;
        }
        public int Id { get; set; }

        public DateTime TimeCreated { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Body { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public string UserId { get; set; }

        public int RestaurantId { get; set; }
    }
}
