﻿using System;
using System.Collections.Generic;

#nullable disable

namespace P1RestaurantReviewer.DataAccess.Entities
{
    public partial class AspNetUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRole Role { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
