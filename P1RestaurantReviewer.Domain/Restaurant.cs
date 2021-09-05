using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
    public class Restaurant
    {
        public Restaurant() { }
        // change to single 
        public Restaurant(int id, string name, string location, int zipCode)
        {
            Id = id;
            Name = name;
            Location = location;
            ZipCode = zipCode;
        }

        public Restaurant(int id, string name, string location, int zipCode, decimal avgRating, List<Review> reviews)
        {
            Id = id;
            Name = name;
            Location = location;
            ZipCode = zipCode;
            AverageRating = avgRating;
            Reviews = reviews;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        //public double AvgRating => Reviews.Average(Reviews.Rating);
        [DisplayName("Zip code")]
        public int ZipCode { get; set; }

        public decimal AverageRating { get; set; }
        public List<Review> Reviews {get; set; }
    }
}
