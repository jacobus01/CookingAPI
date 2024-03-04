using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Service.DAL.Models
{
    public class RecipeIngredient: TableBase
    {
        public int RecipeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
