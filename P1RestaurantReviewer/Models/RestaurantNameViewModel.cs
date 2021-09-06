using Microsoft.AspNetCore.Mvc.Rendering;
using P1RestaurantReviewer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    /// <summary>
    /// This is displayed on Restaurant Index page to search and filter restaurants
    /// </summary>
    public class RestaurantNameViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        [DisplayName("Zip Code")]
        public SelectList ZipCode { get; set; }

        public string RestZipCodes { get; set; }

        public string SearchString { get; set; }

    }
}
