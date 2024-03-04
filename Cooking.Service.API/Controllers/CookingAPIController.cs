using AutoMapper;
using Cooking.Service.DAL;
using Cooking.Service.DAL.Models;
using Cooking.Service.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookingAPIController : ControllerBase
    {
        //So usually I would create a repository project with an interface and an implemetation to abstact away the DbContext. Sometimes I would even use a unit of work pattern
        //but for the sake of keeping things simple I will not be doing it in this example
        private readonly CookingDbContext _db;
        private ResponseDTO _response;
        private IMapper _mapper;

        public CookingAPIController(CookingDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetIngredients")]
        public ResponseDTO GetIngredients()
        {
            try
            {
                IEnumerable<IngredientAvailability> objList = _db.Ingredients.ToList();
                _response.Result = _mapper.Map<IEnumerable<IngredientAvailabilityDTO>>(objList);
            }
            catch (Exception ex)
            {
                //This is basic error handeling but it should be sufficient for this exercise
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("GetRecipeIngredientById/{id:int}")]
        public ResponseDTO GetRecipeIngredientById(int id)
        {
            try
            {
                RecipeIngredient obj = _db.RecipeIngredients.FirstOrDefault(c => c.Id == id);
                _response.Result = _mapper.Map<RecipeIngredientDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetRecipeIngredientsByRecipeId/{recipeId:int}")]
        public ResponseDTO GetRecipeIngredientsByRecipeId(int recipeId)
        {
            try
            {
                IEnumerable<RecipeIngredient> objList = _db.RecipeIngredients.Where(c=> c.RecipeId == recipeId) .ToList();
                _response.Result = _mapper.Map<IEnumerable<RecipeIngredientDTO>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetRecipes")]
        public ResponseDTO GetRecipes()
        {
            try
            {
                IEnumerable<Recipe> objList = _db.Recipe.ToList();
                var recipeDTOList = _mapper.Map<IEnumerable<RecipeDTO>>(objList);
                //I have not enabled lazy loading so I need to get the recipe ingredients as well and map them
                foreach (RecipeDTO recipeDTO in recipeDTOList)
                {
                    var recipeIngredientsList = _db.RecipeIngredients.Where(c => c.RecipeId == recipeDTO.Id).ToList();
                    recipeDTO.RecipeIngredients = _mapper.Map<List<RecipeIngredientDTO>>(recipeIngredientsList);
                }

                _response.Result = recipeDTOList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetMaximumPeopleFed")]
        public ResponseDTO GetMaximumPeopleFed()
        {
            try
            {
                IEnumerable<Recipe> recipeList = _db.Recipe.ToList();
                var recipeDTOList = _mapper.Map<List<RecipeDTO>>(recipeList);
                //I have not enabled lazy loading so I need to get the recipe ingredients as well and map them
                foreach (RecipeDTO recipeDTO in recipeDTOList)
                {
                    var recipeIngredientsList = _db.RecipeIngredients.Where(c => c.RecipeId == recipeDTO.Id).ToList();
                    recipeDTO.RecipeIngredients = _mapper.Map<List<RecipeIngredientDTO>>(recipeIngredientsList);
                }

                IEnumerable<IngredientAvailability> ingredientAvailabilities = _db.Ingredients.ToList();
                var ingredientAvailabilitiesDTO = _mapper.Map<List<IngredientAvailabilityDTO>>(ingredientAvailabilities);

                RecipeOptimizer.MaximizePeopleFed(recipeDTOList, ingredientAvailabilitiesDTO);

                _response.Result = RecipeOptimizer.MaximizePeopleFed(recipeDTOList, ingredientAvailabilitiesDTO);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetIngredientById/{id:int}")]
        public ResponseDTO GetIngredientById(int id)
        {
            try
            {
                IngredientAvailability obj = _db.Ingredients.First(c => c.Id == id);
                _response.Result = _mapper.Map<IngredientAvailabilityDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetIngredientByName/{name}")]
        public ResponseDTO GetIngredientByName(string name)
        {
            try
            {
                //Convert the name to lower case to eliminate casing issues
                IngredientAvailability obj = _db.Ingredients.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
                _response.Result = _mapper.Map<IngredientAvailabilityDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Route("UpsertIngredient")]
        public ResponseDTO UpsertIngredient([FromBody] IngredientAvailabilityDTO ingredientDTO)
        {
            try
            {
                ingredientDTO.Name = ingredientDTO.Name.Trim(); //Get rid of leading and trailing spaces 


                if (ingredientDTO.Amount <= 0)
                {
                    throw new Exception("The ingredient amount must be greater than zero");
                }

                if (ingredientDTO.Name == string.Empty)
                {
                    throw new Exception("The ingredient must have a name");
                }

                IngredientAvailability otherExistingIngredient = _db.Ingredients.FirstOrDefault(c => c.Id != ingredientDTO.Id && ingredientDTO.Name.ToLower() == c.Name.ToLower());
                if (otherExistingIngredient != null)
                {
                    throw new Exception("An ingredient by that name already exists");
                }
                otherExistingIngredient = null;
                //Check if the ingredient exists.
                IngredientAvailability existingIngredient = _db.Ingredients.FirstOrDefault(c => c.Id == ingredientDTO.Id);

                if (existingIngredient == null)
                {
                    IngredientAvailability ingredient = _mapper.Map<IngredientAvailability>(ingredientDTO);
                    //add ingredient
                    _db.Ingredients.Add(ingredient);
                    _db.SaveChanges(); //insures the new id is returned in the result
                    _response.Result = _mapper.Map<IngredientAvailabilityDTO>(ingredient);
                }
                else
                {
                    existingIngredient.Name = ingredientDTO.Name;
                    existingIngredient.Amount = ingredientDTO.Amount;
                    _db.Ingredients.Update(existingIngredient);
                    _db.SaveChanges();
                    _response.Result = _mapper.Map<IngredientAvailabilityDTO>(existingIngredient);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Route("UpsertRecipe")]
        public ResponseDTO UpsertRecipe([FromBody] RecipeDTO recipeDTO)
        {
            //This operation should take place in a transaction scope
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                recipeDTO.Name = recipeDTO.Name.Trim(); //Get rid of leading and trailing spaces 

                if (recipeDTO.Portions <= 0)
                {
                    throw new Exception("The recipe portions must be greater than zero");
                }

                if (recipeDTO.Name == string.Empty)
                {
                    throw new Exception("The recipe must have a name");
                }

                Recipe otherExistingRecipe = _db.Recipe.FirstOrDefault(c => c.Id != recipeDTO.Id && recipeDTO.Name.ToLower() == c.Name.ToLower());
                if (otherExistingRecipe != null)
                {
                    throw new Exception("A recipe by that name already exists");
                }
                otherExistingRecipe = null;

                //Check if the recipe exists.
                Recipe existingRecipe = _db.Recipe.FirstOrDefault(c => c.Id == recipeDTO.Id);

                if (existingRecipe == null)
                {
                    Recipe recipe = _mapper.Map<Recipe>(recipeDTO);
                    
                    
                    //add recipe
                    _db.Recipe.Add(recipe);
                    _db.SaveChanges(); //This is done to be able to get a RecipeId for the child elements
                    foreach (RecipeIngredientDTO recipeIngredientDTO in recipeDTO.RecipeIngredients)
                    {
                        //We need to include the RecipeId So the mapping needs to be as follow:
                        RecipeIngredient recipeIngredient = new RecipeIngredient();
                        recipeIngredient.RecipeId = recipe.Id;
                        //This needs to be validated before the recipe gets created
                        recipeIngredient.Name = recipeIngredientDTO.Name;
                        //This needs to be validated before the recipe gets created
                        recipeIngredient.Amount = recipeIngredientDTO.Amount;
                        _db.Add(recipeIngredient);
                        _db.SaveChanges();
                        //Map the new recipe ingredient back to the DTO
                        recipeIngredientDTO.Id = recipeIngredient.Id;
                    }

                    _response.Result = _mapper.Map<RecipeDTO>(recipe);

                }
                else
                {
                    existingRecipe.Name = recipeDTO.Name;
                    existingRecipe.Portions = recipeDTO.Portions;
                    _db.Recipe.Update(existingRecipe);
                    foreach (RecipeIngredientDTO recipeIngredientDTO in recipeDTO.RecipeIngredients)
                    {
                        //We need to determine if there is already a recipe ingredient
                        RecipeIngredient existingRecipeIngredient = _db.RecipeIngredients.FirstOrDefault(c=> c.Id== recipeIngredientDTO.Id);
                        if(existingRecipeIngredient == null) 
                        {
                            RecipeIngredient recipeIngredient = new RecipeIngredient();
                            recipeIngredient.RecipeId = existingRecipe.Id;

                            //We can't have a recipe ingedient with the same name listed somewhere else on the recipe
                            RecipeIngredient otherRecipeIngredient = _db.RecipeIngredients.FirstOrDefault(c=> c.Name.ToLower() == recipeIngredientDTO.Name.ToLower() && c.RecipeId == existingRecipe.Id && recipeIngredientDTO.Id != recipeIngredientDTO.Id);
                            if(otherRecipeIngredient != null)
                            {
                                throw new Exception("The recipe already contains an ingredient with this name");
                            }
                            //I do this for my own sanity
                            otherRecipeIngredient = null;

                            recipeIngredient.Name = recipeIngredientDTO.Name;
                            //This needs to be validated before the recipe gets created/updated
                            if(recipeIngredientDTO.Amount <= 0)
                            {
                                throw new Exception("The recipe ingredient amount must be greater than zero");
                            }

                            recipeIngredient.Amount = recipeIngredientDTO.Amount;
                            _db.Add(recipeIngredient);
                            _db.SaveChanges();
                            //Map the new recipe ingredient back to the DTO
                            recipeIngredientDTO.Id = recipeIngredient.Id;
                        }
                        else
                        {
                            //We can't have a recipe ingedient with the same name listed somewhere else on the recipe
                            RecipeIngredient otherRecipeIngredient = _db.RecipeIngredients.FirstOrDefault(c => c.Name.ToLower() == recipeIngredientDTO.Name.ToLower() && c.RecipeId == existingRecipe.Id && recipeIngredientDTO.Id != recipeIngredientDTO.Id);
                            if (otherRecipeIngredient != null)
                            {
                                throw new Exception("The recipe already contains an ingredient with this name");
                            }
                            //I do this for my own sanity
                            otherRecipeIngredient = null;

                            existingRecipeIngredient.Name = recipeIngredientDTO.Name;

                            if (recipeIngredientDTO.Amount <= 0)
                            {
                                throw new Exception("The recipe ingredient amount must be greater than zero");
                            }

                            existingRecipeIngredient.Amount = recipeIngredientDTO.Amount;
                            _db.Update(existingRecipeIngredient);
                            _db.SaveChanges();
                        }

                    }
                    _response.Result = _mapper.Map<RecipeDTO>(existingRecipe);
                }
                //We commit the transaction
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Route("UpsertRecipeIngredient")]
        public ResponseDTO UpsertRecipeIngredient([FromBody] RecipeIngredientDTO recipeIngredientDTO)
        {
            try
            {
                recipeIngredientDTO.Name = recipeIngredientDTO.Name.Trim(); //Get rid of leading and trailing spaces 
                Recipe recipe = _db.Recipe.FirstOrDefault(c => c.Id == recipeIngredientDTO.RecipeId);

                if (recipe == null)
                {
                    throw new Exception("No recipe could be found for the provided recipeId");
                }

                if (recipeIngredientDTO.Amount <= 0)
                {
                    throw new Exception("The recipe ingredient amount must be greater than zero");
                }

                if (recipeIngredientDTO.Name == string.Empty)
                {
                    throw new Exception("The recipe ingredient must have a name");
                }

                RecipeIngredient otherExistingRecipeIngredient = _db.RecipeIngredients.FirstOrDefault(c => c.Id != recipeIngredientDTO.Id && recipeIngredientDTO.Name.ToLower() == c.Name.ToLower() && recipeIngredientDTO.RecipeId == c.RecipeId);
                if (otherExistingRecipeIngredient != null)
                {
                    throw new Exception("A recipe ingredient by that name already exists for this recipe");
                }
                otherExistingRecipeIngredient = null;
                //Check if the recipe ingredient exists for this recipe.
                RecipeIngredient existingRecipeIngredient = _db.RecipeIngredients.FirstOrDefault(c => c.Id == recipeIngredientDTO.Id && c.RecipeId == recipeIngredientDTO.RecipeId);

                if (existingRecipeIngredient == null)
                {
                    RecipeIngredient recipeIngredient = _mapper.Map<RecipeIngredient>(recipeIngredientDTO);
                    //add recipe ingredient
                    _db.RecipeIngredients.Add(recipeIngredient);
                    _db.SaveChanges(); //insures the new id is returned in the result
                    _response.Result = _mapper.Map<RecipeIngredient>(recipeIngredient);
                }
                else
                {
                    existingRecipeIngredient.Name = recipeIngredientDTO.Name;
                    existingRecipeIngredient.Amount = recipeIngredientDTO.Amount;
                    _db.RecipeIngredients.Update(existingRecipeIngredient);
                    _db.SaveChanges();
                    _response.Result = _mapper.Map<RecipeIngredientDTO>(existingRecipeIngredient);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("DeleteRecipeById/{id:int}")]
        public ResponseDTO DeleteRecipeById(int id)
        {
            try
            {
                Recipe obj = _db.Recipe.FirstOrDefault(c => c.Id == id);
                if (obj == null)
                {
                    throw new Exception("No recipe by that Id could be found");
                }

                //Delete the recipe ingredients first
                List<RecipeIngredient> recipeIngredients = _db.RecipeIngredients.Where(c => c.RecipeId == id).ToList();
                if(recipeIngredients != null)
                {
                    _db.RecipeIngredients.RemoveRange(recipeIngredients);
                }
                //Delete the recipe
                _db.Recipe.Remove(obj);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("DeleteRecipeIngredientById/{id:int}")]
        public ResponseDTO DeleteRecipeIngredientById(int id)
        {
            try
            {
                RecipeIngredient obj = _db.RecipeIngredients.FirstOrDefault(c => c.Id == id);
                if (obj == null)
                {
                    throw new Exception("No recipe ingredient by that Id could be found");
                }


                //Delete the recipe ingredient
                _db.RecipeIngredients.Remove(obj);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("DeleteIngredientById/{id:int}")]
        public ResponseDTO DeleteIngredientById(int id)
        {
            try
            {
                IngredientAvailability obj = _db.Ingredients.FirstOrDefault(c => c.Id == id);
                if (obj == null)
                {
                    throw new Exception("No ingredient by that Id could be found");
                }


                //Delete the ingredient
                _db.Ingredients.Remove(obj);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
