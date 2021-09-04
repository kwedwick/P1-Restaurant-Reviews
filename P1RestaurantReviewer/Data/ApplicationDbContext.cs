using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using P1RestaurantReviewer.Domain;
using P1RestaurantReviewer.Models;

namespace P1RestaurantReviewer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<P1RestaurantReviewer.Domain.Review> Review { get; set; }
        public DbSet<P1RestaurantReviewer.Domain.Restaurant> Restaurant { get; set; }
        public DbSet<P1RestaurantReviewer.Models.UserRole> UserRole { get; set; }
    }
}
