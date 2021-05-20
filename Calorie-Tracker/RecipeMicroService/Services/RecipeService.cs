using AutoMapper;
using Marvin.JsonPatch;
using RecipeMicroService.Contracts;
using RecipeMicroService.DataTransferObjects;
using RecipeMicroService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMicroService.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RecipeForReadDto> CreateRecipeForUserProfileAsync(Guid id, RecipeForCreateDto recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            recipe.UserProfileId = id;
            _repository.CreateRecipe(recipe);
            var recipeView = _mapper.Map<RecipeForReadDto>(recipe);
            await _repository.SaveAsync();
            return recipeView;
        }

        public async Task<MessageDetailsDto> DeleteRecipeAsync(Guid recipeId)
        {
            var recipe = await _repository.GetRecipeAsync(recipeId, trackChanges: false);
            if (recipe == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Recipe with id: {recipeId} doesn't exist in the database" };
            }
            _repository.DeleteRecipe(recipe);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<RecipeForReadDto> GetRecipeAsync(Guid recipeId)
        {
            var recipe = await _repository.GetRecipeAsync(recipeId, trackChanges: false);
            if (recipe == null)
            {
                return null;
            }
            var recipeDto = _mapper.Map<RecipeForReadDto>(recipe);
            return recipeDto;
        }

        public async Task<int> GetRecipesCount(Guid id)
        {
            return await _repository.CountOfRecipesAsync(id, false);
        }

        public async Task<IEnumerable<RecipeForReadDto>> GetRecipesForUserProfilePaginationAsync(Guid id, int pageSize, int number)
        {
            var recipes = await _repository.GetAllRecipesForUserPaginationAsync(id, pageSize, number, trackChanges: false);
            var recipesDto = _mapper.Map<IEnumerable<RecipeForReadDto>>(recipes);
            return recipesDto;
        }

        public async Task<MessageDetailsDto> PartiallyUpdateRecipeAsync(Guid recipeId, JsonPatchDocument<RecipeForUpdateDto> patchDoc)
        {
            var recipe = await _repository.GetRecipeAsync(recipeId, trackChanges: true);
            if (recipe == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Recipe with id: {recipeId} doesn't exist in the database" };
            }
            var recipeToPatch = _mapper.Map<RecipeForUpdateDto>(recipe);
            patchDoc.ApplyTo(recipeToPatch);
            _mapper.Map(recipeToPatch, recipe);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }

        public async Task<MessageDetailsDto> UpdateRecipeAsync(Guid recipeId, RecipeForUpdateDto recipeDto)
        {
            var recipe = await _repository.GetRecipeAsync(recipeId, trackChanges: true);
            if (recipe == null)
            {
                return new MessageDetailsDto { StatusCode = 404, Message = $"Recipe with id: {recipeId} doesn't exist in the database" };
            }
            _mapper.Map(recipeDto, recipe);
            await _repository.SaveAsync();
            return new MessageDetailsDto { StatusCode = 204 };
        }
    }
}
