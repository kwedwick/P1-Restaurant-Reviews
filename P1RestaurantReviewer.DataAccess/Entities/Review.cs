using System;
using System.Collections.Generic;

#nullable disable

namespace P1RestaurantReviewer.DataAccess.Entities
{
    public partial class Review
    {
        public Review()
        {
            ReviewJoins = new HashSet<ReviewJoin>();
        }

        public int Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }

        public virtual ICollection<ReviewJoin> ReviewJoins { get; set; }
    }
}
