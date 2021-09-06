using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    /// <summary>
    /// This model is used for getting a Role and a list of Users who are in it
    /// </summary>
    public class UserRole
    {
        public string Id { get; set; }
        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}
