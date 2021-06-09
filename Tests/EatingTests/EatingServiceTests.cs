using AutoMapper;
using EatingMicroService.Contracts;
using EatingMicroService.DataTransferObjects;
using EatingMicroService.Models;
using EatingMicroService.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EatingTests
{
    public class EatingServiceTests : IDisposable
    {
        private readonly Guid userId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e");

        Mock<IEatingRepository> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public EatingServiceTests()
        {
            mockRepo = new Mock<IEatingRepository>();
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
        public async void GetAllEatingsForUser_ReturnsZeroItems_WhenDBEmpty()
        {
            mockRepo.Setup(x => x.GetAllEatingsForUserForDateAsync(userId, DateTime.Now.Date, false))
                .ReturnsAsync(GetEatingsForUser(0));
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.GetEatingsForUserProfileForDateAsync(userId);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetEatingForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Null(result);
        }

        [Fact]
        public async void GetEatingForUser_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Eating
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Moment = DateTime.Now,
                        UserProfileId = userId,
                        IngredientsWithGrams = new List<IngredientEating>()
                    }
                );
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.IsType<EatingForReadDto>(result);
        }

        [Fact]
        public async void CreateEatingForUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Eating
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Moment = DateTime.Now,
                        UserProfileId = userId,
                        IngredientsWithGrams = new List<IngredientEating>()
                    }
                );
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.CreateEatingForUserProfileAsync(userId,
                new EatingForCreateDto
                {
                    Name = "Test",
                    Moment = DateTime.Now,
                    IngredientsWithGrams = new List<IngredientEatingForCreateDto>()
                });
            Assert.IsType<EatingForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async void UpdateEatingForUser_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.UpdateEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new EatingForUpdateDto { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void UpdateEatingForUser_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                 .ReturnsAsync
                (
                    new Eating
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Moment = DateTime.Now,
                        UserProfileId = userId,
                        IngredientsWithGrams = new List<IngredientEating>()
                    }
                );
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.UpdateEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new EatingForUpdateDto { });
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateEatingForUser_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<EatingForUpdateDto> { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateEatingForUser_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
               .ReturnsAsync
               (
                   new Eating
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Eating",
                       Moment = DateTime.Now,
                       UserProfileId = userId,
                       IngredientsWithGrams = new List<IngredientEating>()
                   }
               );
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<EatingForUpdateDto> { });
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void DeleteEatingForUser_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.DeleteEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void DeleteEatingForUser_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync
               (
                   new Eating
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Eating",
                       Moment = DateTime.Now,
                       UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                       IngredientsWithGrams = new List<IngredientEating>()
                   }
               );
            var service = new EatingService(mockRepo.Object, mapper);
            var result = await service.DeleteEatingAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(204, result.StatusCode);
        }

        private IEnumerable<Eating> GetEatingsForUser(int num)
        {
            var eatings = new List<Eating>();
            if (num > 0)
            {
                eatings.Add(
                    new Eating
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Moment = DateTime.Now,
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        IngredientsWithGrams = new List<IngredientEating>()
                    });
            }
            return eatings;
        }
    }
}
