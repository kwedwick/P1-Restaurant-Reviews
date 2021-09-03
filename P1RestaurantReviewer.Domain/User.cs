using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
    public interface IUser
    {

        string Id { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }
    public class User : IUser
    {

        public User() { }

        public User(string id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }

        public User(string id, string username, string email, string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
        }
        public User(string id, string username, string email, List<Review> reviews)
        {
            Id = id;
            Username = username;
            Email = email;
            Reviews = reviews;
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public List<Review> Reviews {get; set; }
    }

}
