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
                restaurant => new Domain.Restaurant(restaurant.Id, restaurant.Name, restaurant.Location, restaurant.Zipcode)
            ).ToList();
        }

        public Domain.Restaurant GetRestaurantByName(string name)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants.FirstOrDefault(restaurant => restaurant.Name == name);

            if (foundRestaurant != null)
            {
                return new Domain.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Zipcode);
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

        public Domain.Restaurant UpdateRestaurant(int id, Domain.Restaurant restaurant)
        {
            var updateRestaurant = new Entities.Restaurant
            {
                Id = id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                Zipcode = restaurant.ZipCode
            };
            _context.Restaurants.Update(updateRestaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Domain.Restaurant GetRestaurantById(int id)
        {

            /*return _context.Restaurants
                .Where(rs => rs.Id == id)
                .Include(r => r.ReviewJoins)
                .ThenInclude(j => j.Review)
                .Select(rj => new Domain.Restaurant
                {
                    Id = rj.Id,
                    Name = rj.Name,
                    Location = rj.Location,
                    ZipCode = rj.Zipcode,
                    Reviews = rj.ReviewJoins.Select(k => new Domain.Review(k.Review.Id, k.Review.Title, k.Review.Body, k.Review.Rating)).ToList()
                })
                .ToList();*/

            /*var restaurants = _context.Restaurants
                .Where(rst => rst.Id == id)
                .Include(r => r.ReviewJoins)
                    .ThenInclude(j => j.Review)
                .ToList();

            foreach (var entity in restaurants)
            {
                var r = new Domain.Restaurant(entity.Id, entity.Name, entity.Location, entity.Zipcode);
                r.Reviews.AddRange(entity.ReviewJoins.Select(j => new Domain.Review(j.Review.Id, j.Review.Title, j.Review.Body, j.Review.Rating)));
            }*/

            Entities.Restaurant foundRestaurant = _context.Restaurants.FirstOrDefault(restaurant => restaurant.Id == id);
            if (foundRestaurant != null)
            {
                return new Domain.Restaurant(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, foundRestaurant.Zipcode);
            }
            return new Domain.Restaurant();
        }

        public List<Domain.Restaurant> GetRestaurantByZipcode(int zipcode)
        {
            List<Domain.Restaurant> foundRestaurant = _context.Restaurants.Where(restaurant => restaurant.Zipcode == zipcode).Select(
                rz => new Domain.Restaurant
                {
                    Id = rz.Id,
                    Name = rz.Name,
                    Location = rz.Location,
                    ZipCode = rz.Zipcode
                }
                ).ToList();

            if (foundRestaurant != null)
            {
                return foundRestaurant; 
            }
            return new List<Domain.Restaurant>();
        }

        public Domain.Restaurant DeleteRestaurantById(int id)
        {
            var restaurant = _context.Restaurants.Single(r => r.Id == id);
            if(restaurant != null)
            {
                _context.Remove(restaurant);
                _context.SaveChanges();
                return new Domain.Restaurant();
            }

            return new Domain.Restaurant();
        }
    }
}
