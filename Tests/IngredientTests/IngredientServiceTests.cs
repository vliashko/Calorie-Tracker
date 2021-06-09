using AutoMapper;
using IngredientMicroService.Contracts;
using IngredientMicroService.DataTransferObjects;
using IngredientMicroService.Models;
using IngredientMicroService.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IngredientTests
{
    public class IngredientServiceTests : IDisposable
    {
        Mock<IIngredientRepository> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public IngredientServiceTests()
        {
            mockRepo = new Mock<IIngredientRepository>();
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
            mockRepo.Setup(x => x.GetAllIngredientsPaginationAsync(1, 5, "", false))
                .ReturnsAsync(GetIngredients(0));
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.GetIngredientsPaginationAsync(1, 5, "");
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllIngredients_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.GetAllIngredientsPaginationAsync(1, 5, "", false))
                .ReturnsAsync(GetIngredients(1));   
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.GetIngredientsPaginationAsync(1, 5, "");
            Assert.Single(result);
        }

        [Fact]
        public async void GetIngredient_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Null(result);
        }

        [Fact]
        public async void GetIngredient_ReturnsCorrectTypeAndObject_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
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
            var service = new IngredientService(mockRepo.Object,  mapper);
            var result = await service.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal("Potato", result.Name);
            Assert.IsType<IngredientForReadDto>(result);
        }

        [Fact]
        public async void CreateIngredient_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
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
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.CreateIngredientAsync(new IngredientForCreateDto
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
        public async void UpdateIngredient_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.UpdateIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new IngredientForUpdateDto { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void UpdateIngredient_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
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
            var service = new IngredientService(mockRepo.Object,  mapper);
            var result = await service.UpdateIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new IngredientForUpdateDto
            {
                Name = "Test",
                Calories = 100f,
                Carbohydrates = 10f,
                Fats = 0f,
                Proteins = 5f
            });
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateIngredient_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync(() => null);
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                new Marvin.JsonPatch.JsonPatchDocument<IngredientForUpdateDto> { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateIngredient_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
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
            var service = new IngredientService(mockRepo.Object,  mapper);
            var result = await service.PartiallyUpdateIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                new Marvin.JsonPatch.JsonPatchDocument<IngredientForUpdateDto> { });
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void DeleteIngredient_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), false))
               .ReturnsAsync(() => null);
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.DeleteIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void DeleteIngredient_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
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
            var service = new IngredientService(mockRepo.Object, mapper);
            var result = await service.DeleteIngredientAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(204, result.StatusCode);
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
