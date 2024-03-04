

using Cooking.Service.DTO;

namespace Cooking.Service.API
{
    public class RecipeOptimizer
    {
        public static int MaximizePeopleFed(List<RecipeDTO> recipes, List<IngredientAvailabilityDTO> availableIngredients)
        {
            //This is the tricky part. Making use of dynamic programming to solve the problem
            int[,] dp = new int[recipes.Count + 1, availableIngredients.Count + 1];

            for (int i = 0; i <= recipes.Count; i++)
            {
                for (int j = 0; j <= availableIngredients.Count; j++)
                {
                    if (i == 0 || j == 0)
                        dp[i, j] = 0;
                    else
                    {
                        var recipeIngredient = recipes[i - 1].RecipeIngredients.Find(ing => ing.Name == availableIngredients[j - 1].Name);
                        if (recipeIngredient != null && availableIngredients[j - 1].Amount >= recipeIngredient.Amount)
                        {
                            int remainingIngredients = availableIngredients[j - 1].Amount - recipeIngredient.Amount;
                            dp[i, j] = Math.Max(recipes[i - 1].Portions + dp[i - 1, j], dp[i - 1, j - recipeIngredient.Amount] + recipes[i - 1].Portions);
                        }
                        else
                            dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            return dp[recipes.Count, availableIngredients.Count];
        }
    }
}
