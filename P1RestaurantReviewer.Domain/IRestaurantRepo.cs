using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
        public interface IRestaurantRepo
        {
            List<Restaurant> GetAllRestaurants();

            Restaurant GetRestaurantByName(string name);

            Restaurant CreateRestaurant(Restaurant restaurant);

            List<Restaurant> GetRestaurantByZipcode(int id);

            Restaurant UpdateRestaurant(int id, Restaurant restaurant);

            Restaurant GetRestaurantById(int id);

            Restaurant DeleteRestaurantById(int id);
        }
}

