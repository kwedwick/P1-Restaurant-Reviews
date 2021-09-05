using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Models
{
    /// <summary>
    /// This is used to represent the role and the details of the users who are in or not in the role
    /// </summary>
    public class RoleEdit
    {
        public string Id { get; set; }
        public IdentityRole Role { get; set; }
        public IEnumerable<IdentityUser> Members { get; set; }
        public IEnumerable<IdentityUser> NonMembers { get; set; }
    }
}
