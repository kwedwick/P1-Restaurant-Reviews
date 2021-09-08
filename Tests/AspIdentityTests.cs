/*using Xunit;
using Entity = P1RestaurantReviewer.DataAccess.Entities;
using P1RestaurantReviewer;
using P1RestaurantReviewer.DataAccess;
using P1RestaurantReviewer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tests
{
    class AspIdentityTests
    {
        private readonly DbContextOptions<P1RestaurantReviewer.Data.ApplicationDbContext> options;

        public AspIdentityTests()
        {
            options = new DbContextOptionsBuilder<P1RestaurantReviewer.Data.ApplicationDbContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }



        private void Seed()
        {
            using (var context = new P1RestaurantReviewer.Data.ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.User.AddRange(
                    new User
                    {
                        Id = "abc123",
                        Email = "kwedwick@gmail.com",
                        Username = "kwedwick",
                        Password = "password1234",
                    }
                    ,
                    new Entity.AspNetUser
                    {
                        Id = "123abc",
                        Email = "bwedwick@yahoo.com",
                        UserName = "bwedwick",
                        PasswordHash = "password1234",
                    },
                    new Entity.AspNetUser
                    {
                        Id = "jkl123",
                        Email = "lhagen@outlook.com",
                        UserName = "lhagen",
                        PasswordHash = "password1234",

                    },
                    new Entity.AspNetUser
                    {
                        Id = "123jkl",
                        Email = "tneumann@msn.com",
                        UserName = "tneumann",
                        PasswordHash = "password1234",

                    }
                );
         
                context.SaveChanges();
            }
        }

    }
}
*/