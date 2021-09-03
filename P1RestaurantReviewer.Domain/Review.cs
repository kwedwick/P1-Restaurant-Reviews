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

        public Review(int id, DateTime dateTime, string title, string body, int rating)
        {
            Id = id;
            TimeCreated = dateTime;
            Title = title;
            Body = body;
            Rating = rating;

        }

        public Review(int id, string title, string body, int rating, string username, string restaurantName) : this()
        {
            Id = id;
            Title = title;
            Body = body;
            Rating = rating;
            Username = Username;
            RestaurantName = restaurantName;
        }

        public Review(int id, string title, string body, int rating, string userId, int restaurantId, string username, string restaurantName) : this()
        {
            Id = id;
            Title = title;
            Body = body;
            Rating = rating;
            UserId = userId;
            RestaurantId = restaurantId;
            Username = Username;
            RestaurantName = restaurantName;
        }
        public int Id { get; set; }

        DateTime TimeCreated { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }

        public int RestaurantId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }

        public string RestaurantName { get; set; }
    }
}
