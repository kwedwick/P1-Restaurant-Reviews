using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
    public class Review
    {
        public Review() { }

        public Review(int id, string title, string body, int rating)
        {
            Id = id;
            TimeCreated = DateTime.Now;
            Title = title;
            Body = body;
            Rating = rating;
        }

        public Review(string title, string body, int rating, Restaurant restaurantDetails) : this()
        {
            Title = title;
            Body = body;
            Rating = rating;
            RestaurantDetails = restaurantDetails;
        }
        public int Id { get; set; }

        DateTime TimeCreated { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }

        public Restaurant RestaurantDetails { get; set; }
    }
}
