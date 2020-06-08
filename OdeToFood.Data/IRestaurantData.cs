using OdeToFood.Core;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantByName(string Name);
        Restaurant GetRestaurantByID(int id);
        Restaurant UpdateRestaurant(Restaurant UpdatedRestaurant);
        Restaurant AddRestaurant(Restaurant AddedRestaurant);
        Restaurant Delete(int id);
        int Count();
        int Commit();
    }
}
