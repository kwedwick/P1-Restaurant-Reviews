using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P1RestaurantReviewer.Domain;
using Microsoft.EntityFrameworkCore;
using P1RestaurantReviewer.DataAccess.Entities;


namespace P1RestaurantReviewer.DataAccess
{
    public class RestaurantsRepo : IRestaurantsRepo
    {
        private restaurantreviewerContext _context;

        public RestaurantsRepo(restaurantreviewerContext context)
        {
            _context = context;
        }

        public List<Domain.Restaurants> GetAllRestaurants()
        {
            return _context.Restaurants.Select(
                restaurant => new Domain.Restaurants(restaurant.Id, restaurant.Name, restaurant.Location, (int)restaurant.Zipcode)
            ).ToList();
        }

        public Domain.Restaurants GetRestaurantByName(string name)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants.FirstOrDefault(restaurant => restaurant.Name == name);

            if (foundRestaurant != null)
            {
                return new Domain.Restaurants(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, (int)foundRestaurant.Zipcode);
            }
            return new Domain.Restaurants();
        }

        public Domain.Restaurants CreateRestaurant(Domain.Restaurants restaurant)
        {
            var newEntity = new Entities.Restaurant
            {
                Name = restaurant.Name,
                Location = restaurant.Location,
                Zipcode = restaurant.ZipCode
            };
            _context.Restaurants.Add(newEntity);
            _context.SaveChanges();
            restaurant.Id = newEntity.Id;
            return restaurant;
        }



    }
}
