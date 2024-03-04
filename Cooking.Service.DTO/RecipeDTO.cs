namespace Cooking.Service.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Portions { get; set; }
        public List<RecipeIngredientDTO> RecipeIngredients { get; set; }
    }
}