using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking.Service.DTO
{
    public class RecipeIngredientDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }

        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
