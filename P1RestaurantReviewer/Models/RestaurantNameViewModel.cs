using Microsoft.AspNetCore.Mvc.Rendering;
using P1RestaurantReviewer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    public class RestaurantNameViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        [DisplayName("Zip Code")]
        public SelectList ZipCode { get; set; }

        public string RestZipCodes { get; set; }

        public string SearchString { get; set; }

    }
}
