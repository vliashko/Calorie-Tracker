using AutoMapper;
using Moq;
using RecipeMicroService.Contracts;
using RecipeMicroService.DataTransferObjects;
using RecipeMicroService.Models;
using RecipeMicroService.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace RecipeTests
{
    public class RecipeServiceTests : IDisposable
    {
        private readonly Guid userId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e");

        Mock<IRecipeRepository> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public RecipeServiceTests()
        {
            mockRepo = new Mock<IRecipeRepository>();
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
            mockRepo.Setup(x => x.GetAllRecipesForUserPaginationAsync(userId, 5, 1, false))
                .ReturnsAsync(GetRecipesForUser(0));
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.GetRecipesForUserProfilePaginationAsync(userId, 5, 1);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetRecipe_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Null(result);
        }

        [Fact]
        public async void GetRecipe_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Instruction = "Instruction",
                        UserProfileId = userId,
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    }
                );
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.IsType<RecipeForReadDto>(result);
        }

        [Fact]
        public async void CreateRecipe_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Instruction = "Instruction",
                        UserProfileId = userId,
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    }
                );
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.CreateRecipeForUserProfileAsync(userId,
                new RecipeForCreateDto
                {
                    Name = "Test",
                    Instruction = "Instruction",
                    IngredientsWithGrams = new List<IngredientRecipeForCreateDto>()
                });
            Assert.IsType<RecipeForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async void UpdateRecipe_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.UpdateRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new RecipeForUpdateDto { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void UpdateRecipe_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                 .ReturnsAsync
                (
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Instruction = "Instruction",
                        UserProfileId = userId,
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    }
                );
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.UpdateRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new RecipeForUpdateDto { });
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateRecipe_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<RecipeForUpdateDto> { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateRecipe_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
               .ReturnsAsync
               (
                   new Recipe
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Eating",
                       Instruction = "Instruction",
                       UserProfileId = userId,
                       IngredientsWithGrams = new List<IngredientRecipe>()
                   }
               );
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<RecipeForUpdateDto> { });
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void DeleteRecipe_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.DeleteRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void DeleteRecipe_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync
               (
                   new Recipe
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Eating",
                       Instruction = "Instruction",
                       UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                       IngredientsWithGrams = new List<IngredientRecipe>()
                   }
               );
            var service = new RecipeService(mockRepo.Object, mapper);
            var result = await service.DeleteRecipeAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(204, result.StatusCode);
        }

        private IEnumerable<Recipe> GetRecipesForUser(int num)
        {
            var recipes = new List<Recipe>();
            if (num > 0)
            {
                recipes.Add(
                    new Recipe
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Instruction = "Instruction",
                        UserProfileId = userId,
                        IngredientsWithGrams = new List<IngredientRecipe>()
                    });
            }
            return recipes;
        }
    }
}
