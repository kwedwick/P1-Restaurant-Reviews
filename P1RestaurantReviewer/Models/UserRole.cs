using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    public class UserRole
    {
        public string Id { get; set; }
        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}
