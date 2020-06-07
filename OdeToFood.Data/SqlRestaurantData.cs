using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;
namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }


        public Restaurant AddRestaurant(Restaurant AddedRestaurant)
        {
            db.Add(AddedRestaurant);
            return AddedRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantByID(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetRestaurantByID(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string Name)
        {
            var query = from r in db.Restaurants
                        where string.IsNullOrEmpty(Name) || r.Name.StartsWith(Name.ToUpper())
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant UpdateRestaurant(Restaurant UpdatedRestaurant)
        {
            var entity = db.Restaurants.Attach(UpdatedRestaurant);
            entity.State = EntityState.Modified;
            return UpdatedRestaurant;
        }
    }
}
