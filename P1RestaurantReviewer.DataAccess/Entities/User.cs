using System;
using System.Collections.Generic;

#nullable disable

namespace P1RestaurantReviewer.DataAccess.Entities
{
    public partial class User
    {
        public User()
        {
            ReviewJoins = new HashSet<ReviewJoin>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }

        public virtual ICollection<ReviewJoin> ReviewJoins { get; set; }
    }
}
