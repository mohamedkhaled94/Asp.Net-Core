using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Text;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        [Required]
        public int ID { get; set; }
        [Required , StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string Location { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
