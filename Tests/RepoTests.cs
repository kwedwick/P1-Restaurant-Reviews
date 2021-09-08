using Xunit;
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
    public class RepoTests
    {
        
        private readonly DbContextOptions<Entity.restaurantreviewerContext> options;

        public RepoTests()
        {
            options = new DbContextOptionsBuilder<Entity.restaurantreviewerContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }

        [Fact]
        public void GetAllUsersShouldGetAllUsers()
        {
            //Given
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                IUserRepo _repo = new UserRepo(context);
                //When
                var users = _repo.GetAllMembers();
                //Then
                Assert.Equal(4, users.Count);
            }
        }

        [Fact]
        public void GetAllRestuarantsShouldGetAllRestaurants()
        {
            //Given
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                IRestaurantRepo _repo = new RestaurantRepo(context);
                //When
                var restaurants = _repo.GetAllRestaurants();
                //Then
                Assert.Equal(4, restaurants.Count);
            }
        }

        [Fact]
        public void GetAllReviewsShouldGetAllReviews()
        {
            //Given
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                IReviewRepo _repo = new ReviewRepo(context);
                //When
                var reviews = _repo.GetAllReviews();
                //Then
                Assert.Equal(4, reviews.Count);
            }
        }

       /* [Fact]
        public void CreateUserShouldCreateAUser()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                *//* UserManager<IdentityUser> _repo = new UserManager(arrangeContext);*//*
                UserManager<IdentityUser> _repo = 

                //Act
                _repo.CreateAsync(
                    new IdentityUser
                    {
                        Id = "asdj12",
                        Email = "bsmith@gmail.com",
                        UserName = "bsmith",
                        PasswordHash = "password1234",

                    }
                );
            }

            using (var assertContext = new Entity.restaurantreviewerContext(options))
            {

                Entity.AspNetUser user = assertContext.AspNetUsers.FirstOrDefault(user => user.Id == "asdj12");

                Assert.NotNull(user);
                Assert.Equal("Bob", user?.UserName);
            }
        }*/

        [Fact]
        public void CreateRestaurantShouldCreateARestaurant()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IRestaurantRepo _repo = new RestaurantRepo(arrangeContext);

                //Act
                _repo.CreateRestaurant(
                    new Restaurant
                    {
                        Id = 5,
                        Name = "Bob's Burgers",
                        Location = "555 Hollywood Ave, Hollywood, CA",
                        ZipCode = 50670
                    }
                );
            }

            using (var assertContext = new Entity.restaurantreviewerContext(options))
            {

                Entity.Restaurant restaurant = assertContext.Restaurants.FirstOrDefault(restaurant => restaurant.Id == 5);

                Assert.NotNull(restaurant);
                Assert.Equal("Bob's Burgers", restaurant?.Name);
            }
        }

        [Fact]
        public void CreateReviewShouldCreateAReview()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IReviewRepo _repo = new ReviewRepo(arrangeContext);

                //Act
                _repo.CreateReview(
                    new Review
                    {
                        Id = 5,
                        Title = "Just okay",
                        Body = "This restaurant was okay but the staff were not as friendly. Karen was extremely rude. Food was good.",
                        Rating = 3,
                        UserId = "jkl123",
                        RestaurantId = 2
                    }
                );
            }

            using (var assertContext = new Entity.restaurantreviewerContext(options))
            {

                Entity.Review review = assertContext.Reviews.FirstOrDefault(review => review.Id == 5);

                Entity.ReviewJoin reviewJoin = assertContext.ReviewJoins.FirstOrDefault(reviewJoin => reviewJoin.Id == 5);

                Assert.NotNull(review);
                Assert.Equal("Just okay", review.Title);

                Assert.NotNull(reviewJoin);
                Assert.Equal("jkl123", reviewJoin.UserId);
            }
        }

        [Fact]
        public void GetRestaurantByNameShouldGetRestaurantByName()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                //Given
                using (var context = new Entity.restaurantreviewerContext(options))
                {
                    IRestaurantRepo _repo = new RestaurantRepo(context);
                    //When
                    var restaurants = _repo.GetRestaurantByName("Culvers");
                    //Then
                    Assert.Equal("Culvers", restaurants.Name);
                    Assert.Equal(1, restaurants.Id);
                }
            }

        }

        [Fact]
        public void LoginShouldReturnUser()
        {
            //Given
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IUserRepo _repo = new UserRepo(arrangeContext);

                //Act
                var loggedInUser =
                _repo.GetUserLogin(
                    new User
                    {
                        Username = "kwedwick",
                        Password = "password1234",
                    }
                );

                Assert.NotNull(loggedInUser);
                Assert.Equal("kwedwick@gmail.com", loggedInUser.Email);
                Assert.Equal("abc123", loggedInUser.Id);
            }
        }

       /* [Fact]
        public void LoginShouldReturnEmptyUser()
        {
            //Given
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IUserRepo _repo = new UserRepo(arrangeContext);

                //Act
                var loggedInUser =
                _repo.GetUserLogin(
                    new User
                    {
                        Email = "pShermin@gmail.com",
                        UserName = "pShermin",
                        PasswordHash = "42wallabyway",
                    }
                );

                Assert.NotNull(loggedInUser);
                Assert.Equal(null, loggedInUser.Email);
                Assert.Equal(0, loggedInUser.Id);
            }
        }*/

        [Fact]
        public void GetReviewsByRestaurantIDShouldReturnAllReviewsForThatRestaurant()
        {
            //Given
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IReviewRepo _repo = new ReviewRepo(arrangeContext);

                //Act
                List<Review> newReviews =
                _repo.GetReviewsbyRestaurantId(3);

                Assert.NotNull(newReviews);
                Assert.Single(newReviews);
            }
        }

        private void Seed()
        {
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AspNetUsers.AddRange(
                    new Entity.AspNetUser
                    {
                        Id = "abc123",
                        Email = "kwedwick@gmail.com",
                        UserName = "kwedwick",
                        PasswordHash = "password1234",
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


                context.Restaurants.AddRange(
                    new Entity.Restaurant
                    {
                        Id = 1,
                        Name = "Culvers",
                        Location = "456 Deer Rd, Madison, WI",
                        Zipcode = 53562,
                    },
                    new Entity.Restaurant
                    {
                        Id = 2,
                        Name = "Subway",
                        Location = "555 Jackson Rd, Los Angeles, CA",
                        Zipcode = 90001,
                    },
                    new Entity.Restaurant
                    {
                        Id = 3,
                        Name = "Pizza Hut",
                        Location = "123 Sesame St, Baltimore City, MD",
                        Zipcode = 21201,
                    },
                    new Entity.Restaurant
                    {
                        Id = 4,
                        Name = "Panera",
                        Location = "789 Greenfield Ave, Chicago, IL",
                        Zipcode = 21201,
                    }
                );

                context.Reviews.AddRange(
                    new Entity.Review
                    {
                        Id = 1,
                        TimeCreated = DateTime.Now,
                        Title = "Just okay",
                        Body = "This restaurant was okay but the staff were not as friendly. Karen was extremely rude. Food was good.",
                        Rating = 3,
                    },
                    new Entity.Review
                    {
                        Id = 2,
                        TimeCreated = DateTime.Now,
                        Title = "OULD EAT AGAIN",
                        Body = "THE STAFF WERE SO NICE AND THE FOOD CAME OUT WARM! WOW! I''M SHOOKETH!",
                        Rating = 5,
                    },
                    new Entity.Review
                    {
                        Id = 3,
                        TimeCreated = DateTime.Now,
                        Title = "Pleasant Experience",
                        Body = "I called in a reservation and was told there would be a 3 day wait. I decided that was okay and the food was good. 1 star missing for the wait time.",
                        Rating = 4,
                    },
                    new Entity.Review
                    {
                        Id = 4,
                        TimeCreated = DateTime.Now,
                        Title = "Great'",
                        Body = "It''s what you expect.",
                        Rating = 4,
                    }
                );

                context.ReviewJoins.AddRange(
                    new Entity.ReviewJoin
                    {
                        Id = 1,
                        ReviewId = 1,
                        RestaurantId = 1,
                        UserId = "abc123",
                    },
                    new Entity.ReviewJoin
                    {
                        Id = 2,
                        ReviewId = 2,
                        RestaurantId = 2,
                        UserId = "123abc",
                    },
                    new Entity.ReviewJoin
                    {
                        Id = 3,
                        ReviewId = 3,
                        RestaurantId = 3,
                        UserId = "jkl123",
                    },
                    new Entity.ReviewJoin
                    {
                        Id = 4,
                        ReviewId = 4,
                        RestaurantId = 4,
                        UserId = "123jkl",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}