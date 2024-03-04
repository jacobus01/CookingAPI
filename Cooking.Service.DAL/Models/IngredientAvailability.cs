using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Service.DAL.Models
{
    public class IngredientAvailability: TableBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
