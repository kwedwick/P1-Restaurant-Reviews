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
    public class RestaurantRepo : IRestaurantRepo
    {
        private restaurantreviewerContext _context;

        public RestaurantRepo(restaurantreviewerContext context)
        {
            _context = context;
        }

        public List<Domain.Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.Select(
                restaurant => new Domain.Restaurant(restaurant.Id, restaurant.Name, restaurant.Location, (int)restaurant.Zipcode)
            ).ToList();
        }

        public Domain.Restaurant GetRestaurantByName(string name)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants.FirstOrDefault(restaurant => restaurant.Name == name);

            if (foundRestaurant != null)
            {
                return new Domain.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, (int)foundRestaurant.Zipcode);
            }
            return new Domain.Restaurant();
        }

        public Domain.Restaurant CreateRestaurant(Domain.Restaurant restaurant)
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

        public Domain.Restaurant UpdateRestaurant(Domain.Restaurant restaurant)
        {
            var updateRestaurant = new Entities.Restaurant
            {
                Name = restaurant.Name,
                Location = restaurant.Location,
                Zipcode = restaurant.ZipCode
            };
            _context.Restaurants.Add(updateRestaurant);
            _context.SaveChanges();
            return restaurant;
        }
    }
}
