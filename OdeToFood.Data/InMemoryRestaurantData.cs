using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>() {
                new Restaurant {ID=1, Name ="Tuskanini",Location="NitherLand", Cuisine=CuisineType.Italian},
                new Restaurant {ID=2, Name ="Macdonalds",Location="USA", Cuisine=CuisineType.None},
                new Restaurant {ID=3, Name ="Indian Gate",Location="Egypt", Cuisine=CuisineType.Indian},
                new Restaurant {ID=4, Name ="Mexicano",Location="Mexico", Cuisine=CuisineType.Mexican}

            };
        }
        public Restaurant UpdateRestaurant(Restaurant UpdatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.ID == UpdatedRestaurant.ID);
            if (restaurant != null )
            {
                restaurant.Name = UpdatedRestaurant.Name;
                restaurant.Location = UpdatedRestaurant.Location;
                restaurant.Cuisine = UpdatedRestaurant.Cuisine;
            }
            return restaurant;
        }
        public Restaurant AddRestaurant(Restaurant AddedRestaurant)
        {
            AddedRestaurant.ID = restaurants.Max(r => r.ID) + 1;
            restaurants.Add(AddedRestaurant);
            return AddedRestaurant;
        }
        public int Commit()
        {
            return 0;
        }
        public Restaurant GetRestaurantByID(int id )
        {
            return restaurants.SingleOrDefault(r => r.ID == id);
        }
        public IEnumerable<Restaurant> GetRestaurantByName(string name = null)
        {
            return from r in restaurants
                   where  string.IsNullOrEmpty(name) || r.Name.StartsWith(name.ToUpper())
                   orderby r.Name
                   select r;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.ID == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }
    }
}
