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
            
        }
        [Fact]
        public async void GetAllEatingsForUser_ReturnsOneItem_WhenDBHasOneResource()
        {

        }
        [Fact]
        public async void GetAllEatingsForUser_ReturnsNull_WhenNonExistentIDProvided()
        {

        }
        [Fact]
        public async void GetEatingForUser_ReturnsNull_WhenNonExistentIDProvided()
        {

        }
        [Fact]
        public async void GetEatingForUser_ReturnsCorrectType_WhenValidIDProvided()
        {

        }
        [Fact]
        public async void CreateEatingForUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {

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
    }
}
