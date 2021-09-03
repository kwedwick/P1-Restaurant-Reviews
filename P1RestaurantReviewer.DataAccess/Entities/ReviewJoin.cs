using System;
using System.Collections.Generic;

#nullable disable

namespace P1RestaurantReviewer.DataAccess.Entities
{
    public partial class ReviewJoin
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int RestaurantId { get; set; }
        public string UserId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Review Review { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
