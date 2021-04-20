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
    public class IngredientServiceTests : IDisposable
    {
        Mock<IRepositoryManager> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public IngredientServiceTests()
        {
            mockRepo = new Mock<IRepositoryManager>();
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
        public async void GetAllIngredients_ReturnsZeroItems_WhenDBEmpty()
        {
            mockRepo.Setup(x => x.Ingredient.GetAllIngredientsAsync(false))
                .ReturnsAsync(GetIngredients(0));

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetIngredients();

            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllIngredients_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.Ingredient.GetAllIngredientsAsync(false))
                .ReturnsAsync(GetIngredients(1));

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetIngredients();

            Assert.Single(result);
        }
        [Fact]
        public async void GetIngredient_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), false))
                .ReturnsAsync(() => null);

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.Null(result);
        }
        [Fact]
        public async void GetIngredient_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Ingredient
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Potato",
                        Calories = 77f,
                        Proteins = 2f,
                        Fats = 0.4f,
                        Carbohydrates = 16.3f
                    }
                );

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.Equal("Potato", result.Name);
            Assert.IsType<IngredientForReadDto>(result);           
        }
        [Fact]
        public async void CreateIngredient_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Ingredient
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Potato",
                        Calories = 77f,
                        Proteins = 2f,
                        Fats = 0.4f,
                        Carbohydrates = 16.3f
                    }
                );

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.CreateIngredient(new IngredientForCreateDto
            {
                Name = "Test",
                Calories = 100f,
                Carbohydrates = 10f,
                Fats = 0f,
                Proteins = 5f
            });

            Assert.IsType<IngredientForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }
        [Fact]
        public async void UpdateIngredient_ReturnsFalse_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), false))
                .ReturnsAsync(() => null);

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new IngredientForUpdateDto { });

            Assert.False(result);
        }
        [Fact]
        public async void UpdateIngredient_ReturnsTrue_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync
                (
                    new Ingredient
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Potato",
                        Calories = 77f,
                        Proteins = 2f,
                        Fats = 0.4f,
                        Carbohydrates = 16.3f
                    }
                );

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new IngredientForUpdateDto 
            {
                Name = "Test",
                Calories = 100f,
                Carbohydrates = 10f,
                Fats = 0f,
                Proteins = 5f
            });

            Assert.True(result);
        }
        [Fact]
        public async void PartiallyUpdateIngredient_ReturnsFalse_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), false))
               .ReturnsAsync(() => null);

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 
                new Marvin.JsonPatch.JsonPatchDocument<IngredientForUpdateDto> { });

            Assert.False(result);
        }
        [Fact]
        public async void PartiallyUpdateIngredient_ReturnsTrue_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync
                (
                    new Ingredient
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Potato",
                        Calories = 77f,
                        Proteins = 2f,
                        Fats = 0.4f,
                        Carbohydrates = 16.3f
                    }
                );

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 
                new Marvin.JsonPatch.JsonPatchDocument<IngredientForUpdateDto> { });

            Assert.True(result);
        }
        [Fact]
        public async void DeleteIngredient_ReturnsFalse_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), false))
               .ReturnsAsync(() => null);

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.False(result);
        }
        [Fact]
        public async void DeleteIngredient_ReturnsTrue_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.Ingredient.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync
               (
                   new Ingredient
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Potato",
                       Calories = 77f,
                       Proteins = 2f,
                       Fats = 0.4f,
                       Carbohydrates = 16.3f
                   }
               );

            var service = new IngredientService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteIngredient(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.True(result);
        }
        private IEnumerable<Ingredient> GetIngredients(int num)
        {
            var ingredients = new List<Ingredient>();
            if (num > 0)
            {
                ingredients.Add(
                    new Ingredient
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Potato",
                        Calories = 77f,
                        Proteins = 2f,
                        Fats = 0.4f,
                        Carbohydrates = 16.3f
                    });
            }
            return ingredients;
        }
    }
}
