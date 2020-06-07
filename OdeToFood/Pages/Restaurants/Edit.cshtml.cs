using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            { 
                Restaurant = restaurantData.GetRestaurantByID(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();

        }
        public IActionResult OnPost()
        {   
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();  
            }
            if (Restaurant.ID > 0)
            {
                restaurantData.UpdateRestaurant(Restaurant);              
            }
            else
            {
                restaurantData.AddRestaurant(Restaurant);
            }
            TempData["Message"] = "Restaurant Has Saved Succesflly";
            restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.ID });

        }
    }
}