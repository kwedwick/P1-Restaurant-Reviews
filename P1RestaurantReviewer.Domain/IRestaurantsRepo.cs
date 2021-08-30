using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
        public interface IRestaurantsRepo
        {
            List<Restaurants> GetAllRestaurants();

            Restaurants GetRestaurantByName(string name);

            Restaurants CreateRestaurant(Restaurants restaurant);
        }
}

