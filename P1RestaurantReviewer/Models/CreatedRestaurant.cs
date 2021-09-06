using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    /// <summary>
    /// This model is used to take in User Input to submit new Restaurant to DB and validates it
    /// </summary>
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
        [DisplayName("Zip code")]
        public int ZipCode { get; set; }

    }
}
