//using System;
//using System.Collections.Generic;

//public class Recipe
//{
//    public string Name { get; set; }
//    public int Portions { get; set; }
//    public List<RecipeIngredient> RecipeIngredients { get; set; }
//}

//public class RecipeIngredient
//{
//    public string Name { get; set; }
//    public int Amount { get; set; }
//}

//public class IngredientAvailability
//{
//    public string Name { get; set; }
//    public int Amount { get; set; }
//}

//public class RecipeOptimizer
//{
//    public static int MaximizePeopleFed(List<Recipe> recipes, List<IngredientAvailability> availableIngredients)
//    {
//        int[,] dp = new int[recipes.Count + 1, availableIngredients.Count + 1];

//        for (int i = 0; i <= recipes.Count; i++)
//        {
//            for (int j = 0; j <= availableIngredients.Count; j++)
//            {
//                if (i == 0 || j == 0)
//                    dp[i, j] = 0;
//                else
//                {
//                    var recipeIngredient = recipes[i - 1].RecipeIngredients.Find(ing => ing.Name == availableIngredients[j - 1].Name);
//                    if (recipeIngredient != null && availableIngredients[j - 1].Amount >= recipeIngredient.Amount)
//                    {
//                        int remainingIngredients = availableIngredients[j - 1].Amount - recipeIngredient.Amount;
//                        dp[i, j] = Math.Max(recipes[i - 1].Portions + dp[i - 1, j], dp[i - 1, j - recipeIngredient.Amount] + recipes[i - 1].Portions);
//                    }
//                    else
//                        dp[i, j] = dp[i - 1, j];
//                }
//            }
//        }

//        return dp[recipes.Count, availableIngredients.Count];
//    }
//}

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        // Example usage
//        List<Recipe> recipes = new List<Recipe>
//        {
//            new Recipe
//            {
//                Name = "Burger",
//                Portions = 1,
//                RecipeIngredients = new List<RecipeIngredient>
//                {
//                    new RecipeIngredient { Name = "Meat", Amount = 1 },
//                    new RecipeIngredient { Name = "Lettuce", Amount = 1 },
//                    new RecipeIngredient { Name = "Tomato", Amount = 1 },
//                    new RecipeIngredient { Name = "Cheese", Amount = 1 },
//                    new RecipeIngredient { Name = "Dough", Amount = 1 }
//                }
//            },
//            new Recipe
//            {
//                Name = "Pie",
//                Portions = 1,
//                RecipeIngredients = new List<RecipeIngredient>
//                {
//                    new RecipeIngredient{ Name = "Dough", Amount = 2 },
//                    new RecipeIngredient { Name = "Meat", Amount = 2 }
//                }
//            },
//            new Recipe
//            {
//                Name = "Sandwich",
//                Portions = 1,
//                RecipeIngredients = new List<RecipeIngredient>
//                {
//                    new RecipeIngredient { Name = "Dough", Amount = 1 },
//                    new RecipeIngredient { Name = "Cucumber", Amount = 1 }
//                }
//            },
//            new Recipe
//            {
//                Name = "Pasta",
//                Portions = 2,
//                RecipeIngredients = new List<RecipeIngredient>
//                {
//                    new RecipeIngredient { Name = "Dough", Amount = 2 },
//                    new RecipeIngredient { Name = "Tomato", Amount = 1 },
//                    new RecipeIngredient { Name = "Cheese", Amount = 2 },
//                    new RecipeIngredient { Name = "Meat", Amount = 1 }
//                }
//            },
//            new Recipe
//            {
//                Name = "Salad",
//                Portions = 3,
//                RecipeIngredients = new List<RecipeIngredient>
//                {
//                    new RecipeIngredient { Name = "Lettuce", Amount = 2 },
//                    new RecipeIngredient { Name = "Tomato", Amount = 2 },
//                    new RecipeIngredient { Name = "Cucumber", Amount = 1 },
//                    new RecipeIngredient { Name = "Cheese", Amount = 2 },
//                    new RecipeIngredient { Name = "Olives", Amount = 1 }
//                }
//            },
//            new Recipe
//            {
//                Name = "Pizza",
//                Portions = 4,
//                RecipeIngredients = new List<RecipeIngredient>
//                {
//                    new RecipeIngredient { Name = "Dough", Amount = 3 },
//                    new RecipeIngredient { Name = "Tomato", Amount = 2 },
//                    new RecipeIngredient { Name = "Cheese", Amount = 3 },
//                    new RecipeIngredient { Name = "Olives", Amount = 1 }
//                }
//            }
//        };

//        List<IngredientAvailability> availableIngredients = new List<IngredientAvailability>
//        {
//            new IngredientAvailability { Name = "Cucumber", Amount = 2 },
//            new IngredientAvailability { Name = "Olives", Amount = 2 },
//            new IngredientAvailability { Name = "Lettuce", Amount = 3 },
//            new IngredientAvailability { Name = "Meat", Amount = 6 },
//            new IngredientAvailability { Name = "Tomato", Amount = 6 },
//            new IngredientAvailability { Name = "Cheese", Amount =8 },
//            new IngredientAvailability { Name = "Dough", Amount =10 }
//        };

//        int maxPeopleFed = RecipeOptimizer.MaximizePeopleFed(recipes, availableIngredients);
//        Console.WriteLine("Maximum number of people fed: " + maxPeopleFed);
//    }
//}