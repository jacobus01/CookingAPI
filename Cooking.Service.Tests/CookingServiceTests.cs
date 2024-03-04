using Cooking.Service.API;
using Cooking.Service.DTO;

namespace Cooking.Service.Tests
{
    public class CookingServiceTests
    {
        [Fact]
        public void MaximizePeopleFed_Returns_CorrectResult()
        {
            // Arrange
            List<RecipeDTO> recipes = new List<RecipeDTO>
        {
            new RecipeDTO
            {
                Name = "Burger",
                Portions = 1,
                RecipeIngredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { Name = "Meat", Amount = 1 },
                    new RecipeIngredientDTO { Name = "Lettuce", Amount = 1 },
                    new RecipeIngredientDTO { Name = "Tomato", Amount = 1 },
                    new RecipeIngredientDTO { Name = "Cheese", Amount = 1 },
                    new RecipeIngredientDTO { Name = "Dough", Amount = 1 }
                }
            },
            new RecipeDTO
            {
                Name = "Pie",
                Portions = 1,
                RecipeIngredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO{ Name = "Dough", Amount = 2 },
                    new RecipeIngredientDTO { Name = "Meat", Amount = 2 }
                }
            },
            new RecipeDTO
            {
                Name = "Sandwich",
                Portions = 1,
                RecipeIngredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { Name = "Dough", Amount = 1 },
                    new RecipeIngredientDTO { Name = "Cucumber", Amount = 1 }
                }
            },
            new RecipeDTO
            {
                Name = "Pasta",
                Portions = 2,
                RecipeIngredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { Name = "Dough", Amount = 2 },
                    new RecipeIngredientDTO { Name = "Tomato", Amount = 1 },
                    new RecipeIngredientDTO { Name = "Cheese", Amount = 2 },
                    new RecipeIngredientDTO { Name = "Meat", Amount = 1 }
                }
            },
            new RecipeDTO
            {
                Name = "Salad",
                Portions = 3,
                RecipeIngredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { Name = "Lettuce", Amount = 2 },
                    new RecipeIngredientDTO { Name = "Tomato", Amount = 2 },
                    new RecipeIngredientDTO { Name = "Cucumber", Amount = 1 },
                    new RecipeIngredientDTO { Name = "Cheese", Amount = 2 },
                    new RecipeIngredientDTO { Name = "Olives", Amount = 1 }
                }
            },
            new RecipeDTO
            {
                Name = "Pizza",
                Portions = 4,
                RecipeIngredients = new List<RecipeIngredientDTO>
                {
                    new RecipeIngredientDTO { Name = "Dough", Amount = 3 },
                    new RecipeIngredientDTO { Name = "Tomato", Amount = 2 },
                    new RecipeIngredientDTO { Name = "Cheese", Amount = 3 },
                    new RecipeIngredientDTO { Name = "Olives", Amount = 1 }
                }
            }
        };

            List<IngredientAvailabilityDTO> availableIngredients = new List<IngredientAvailabilityDTO>
        {
            new IngredientAvailabilityDTO { Name = "Cucumber", Amount = 2 },
            new IngredientAvailabilityDTO { Name = "Olives", Amount = 2 },
            new IngredientAvailabilityDTO { Name = "Lettuce", Amount = 3 },
            new IngredientAvailabilityDTO { Name = "Meat", Amount = 6 },
            new IngredientAvailabilityDTO { Name = "Tomato", Amount = 6 },
            new IngredientAvailabilityDTO { Name = "Cheese", Amount =8 },
            new IngredientAvailabilityDTO { Name = "Dough", Amount =10 }
        };


            // Act
            int result = RecipeOptimizer.MaximizePeopleFed(recipes, availableIngredients);

            // Assert
            Assert.Equal(9, result);
        }
    }
}