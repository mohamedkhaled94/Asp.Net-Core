using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration Config;
        private readonly IRestaurantData restaurantData;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
            this.Config = config;
        }
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }
        public void OnGet()
        {
            Message = Config["Message"];
            Restaurants = restaurantData.GetRestaurantByName(SearchTerm);
        }
    }
}