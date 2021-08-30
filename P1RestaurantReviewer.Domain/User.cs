using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
    public interface IUser
    {

        int Id { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        int IsAdmin { get; set; }
        string Password { get; set; }
    }
    public class User : IUser
    {

        public User() { }

        public User(int id, string firstName, string lastName, string username, string email, int isAdmin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            IsAdmin = isAdmin;
        }
        public User(int id, string firstName, string lastName, string username, string email, string password, int isAdmin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }

        public User(int id, string firstName, string lastName, string username, string email, int isAdmin, List<Review> reviews)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            IsAdmin = isAdmin;
            Reviews = reviews;
        }

        public User(int id, int isAdmin) : this()
        {
            Id = id;
            IsAdmin = isAdmin;
        }

        public User(int isAdmin) : this()
        {
            IsAdmin = isAdmin;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public int IsAdmin { get; set; }

        public List<Review> Reviews {get; set; }
    }

}
