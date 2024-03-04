using AutoMapper;
using Cooking.Service.DAL.Models;
using Cooking.Service.DTO;

namespace Cooking.Service.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<IngredientAvailabilityDTO, IngredientAvailability>();
                config.CreateMap<IngredientAvailability, IngredientAvailabilityDTO>();
                config.CreateMap<RecipeIngredientDTO, RecipeIngredient>();
                config.CreateMap<RecipeIngredient, RecipeIngredientDTO>();
                config.CreateMap<RecipeDTO, Recipe>();
                config.CreateMap<Recipe, RecipeDTO>();
            });
            return mappingConfig;
        }
    }
}
