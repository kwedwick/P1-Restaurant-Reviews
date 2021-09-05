using System;
using System.Collections.Generic;

#nullable disable

namespace P1RestaurantReviewer.DataAccess.Entities
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            ReviewJoins = new HashSet<ReviewJoin>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Zipcode { get; set; }

        public virtual ICollection<ReviewJoin> ReviewJoins { get; set; }
    }
}
