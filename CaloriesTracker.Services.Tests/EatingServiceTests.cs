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
    public class EatingServiceTests : IDisposable
    {
        Mock<IRepositoryManager> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public EatingServiceTests()
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
        public async void GetAllEatingsForUser_ReturnsZeroItems_WhenDBEmpty()
        {
            mockRepo.Setup(x => x.Eating.GetAllEatingsForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(GetEatingsForUser(0));

            var service = new EatingService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetEatings(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllEatingsForUser_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.Eating.GetAllEatingsForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(GetEatingsForUser(1));

            var service = new EatingService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetEatings(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.Single(result);
        }
        [Fact]
        public async void GetAllEatingsForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Eating.GetAllEatingsForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"), false))
                .ReturnsAsync(() => null);

            var service = new EatingService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetEatings(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"));

            Assert.Null(result);
        }
        [Fact]
        public async void GetEatingForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Eating.GetAllEatingsForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"), false))
                .ReturnsAsync(() => null);

            var service = new EatingService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetEating(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"), 
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.Null(result);
        }
        [Fact]
        public async void GetEatingForUser_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Eating.GetEatingForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), 
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
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
            var service = new EatingService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetEating(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.IsType<EatingForReadDto>(result);
        }
        [Fact]
        public async void CreateEatingForUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
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

            mockRepo.Setup(x => x.Eating.GetEatingForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Eating
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Eating",
                        Moment = DateTime.Now,
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                        IngredientsWithGrams = new List<IngredientEating>()
                    }
                );

            var service = new EatingService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.CreateEating(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
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
        public async void UpdateEatingForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {

        }
        [Fact]
        public async void UpdateEatingForUser_ReturnsTrue_WhenValidIDProvided()
        {

        }
        [Fact]
        public async void PartiallyEatingForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {

        }
        [Fact]
        public async void PartiallyEatingForUser_ReturnsTrue_WhenValidIDProvided()
        {

        }
        [Fact]
        public async void DeleteEatingForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {

        }
        [Fact]
        public async void DeleteEatingForUser_ReturnsTrue_WhenValidIDProvided()
        {

        }
        public IEnumerable<Eating> GetEatingsForUser(int num)
        {
            var eatings = new List<Eating>();
            if(num > 0)
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
