using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.LoggerService;
using CaloriesTracker.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CaloriesTracker.Services.Tests
{
    public class RecipeServiceTests : IDisposable
    {
        Mock<IRepositoryManager> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public RecipeServiceTests()
        {
            mockRepo = new Mock<IRepositoryManager>();
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        Weight = 84f,
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 174,
                    }
                );
            profile = new MappingProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);
        }
        public void Dispose()
        {
            mockRepo = null;
            profile = null;
            configuration = null;
            mapper = null;
        }
        [Fact]
        public async void GetAllRecipesForUser_ReturnsZeroItems_WhenDBEmpty()
        {
            mockRepo.Setup(x => x.Recipe.GetAllRecipesForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(GetRecipesForUser(0));

            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetRecipes(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllRecipesForUser_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.Recipe.GetAllRecipesForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(GetRecipesForUser(1));

            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetRecipes(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.Single(result);
        }
        [Fact]
        public async void GetAllRecipesForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetAllRecipesForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"), false))
                .ReturnsAsync(() => null);

            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetRecipes(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"));

            Assert.Null(result);
        }
        [Fact]
        public async void GetRecipeForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);

            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.Null(result);
        }
        [Fact]
        public async void GetRecipeForUser_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Recipe",
                        Instruction = "Instruction",
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    }
                );
            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.IsType<RecipeForReadDto>(result);
        }
        [Fact]
        public async void CreateRecipeForUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"), true))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                        Weight = 84f,
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 174,
                    }
                );

            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Recipe",
                        Instruction = "Instruction",
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    }
                );

            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.CreateRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new RecipeForCreateDto
                {
                    Name = "Test",
                    Instruction = "Test",  
                    IngredientsWithGrams = new List<IngredientRecipeForCreateDto>()
                });
            Assert.IsType<RecipeForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }
        [Fact]
        public async void UpdateRecipeForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new RecipeForUpdateDto { });
            Assert.False(result);
        }
        [Fact]
        public async void UpdateRecipeForUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync
                (
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Recipe",
                        Instruction = "Intsruction",
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    }
                );
            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new RecipeForUpdateDto { });
            Assert.True(result);
        }
        [Fact]
        public async void PartiallyUpdateRecipeForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<RecipeForUpdateDto> { });
            Assert.False(result);
        }
        [Fact]
        public async void PartiallyUpdateRecipeForUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
               new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
               .ReturnsAsync
               (
                   new Recipe
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Recipe",
                       Instruction = "Instruction",
                       UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                       IngredientsWithGrams = new List<IngredientRecipe>()
                   }
               );
            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<RecipeForUpdateDto> { });
            Assert.True(result);
        }
        [Fact]
        public async void DeleteRecipeForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.False(result);
        }
        [Fact]
        public async void DeleteRecipeForUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Recipe.GetRecipeForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
               new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync
               (
                   new Recipe
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Recipe",
                       Instruction = "Instruction",
                       UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                       IngredientsWithGrams = new List<IngredientRecipe>()
                   }
               );
            var service = new RecipeService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteRecipe(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.True(result);
        }
        private IEnumerable<Recipe> GetRecipesForUser(int num)
        {
            var recipes = new List<Recipe>();
            if(num > 0)
            {
                recipes.Add(
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Recipe",
                        Instruction = "Instruction",
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    });
            }
            return recipes;
        }
    }
}
