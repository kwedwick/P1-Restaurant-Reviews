using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    public class CreatedRestaurant
    {
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }

    }
}
