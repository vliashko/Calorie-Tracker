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
    public class UserServiceTests : IDisposable
    {
        Mock<IRepositoryManager> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public UserServiceTests()
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
        public async void GetAllUsers_ReturnsZeroItems_WhenDBEmpty()
        {
            mockRepo.Setup(x => x.User.GetAllUsersAsync(false)).ReturnsAsync(GetUsers(0));
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetUsers();
            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllUsers_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.User.GetAllUsersAsync(false)).ReturnsAsync(GetUsers(1));
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetUsers();
            Assert.Single(result);
        }
        [Fact]
        public async void GetUserById_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Null(result);
        }
        [Fact]
        public async void GetUserById_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 175,
                        Weight = 85,
                        UserId = "c9d4c053-49b6-410c-bc78-2d54a9991871"
                    }
                );
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.IsType<UserProfileForReadDto>(result);
        }
        [Fact]
        public async void CreateUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 175,
                        Weight = 85,
                        UserId = "c9d4c053-49b6-410c-bc78-2d54a9991871"
                    }
                );

            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.CreateUser(new UserProfileForCreateDto
            {
                DateOfBirth = DateTime.Now,
                Gender = 0,
                Height = 176,
                Weight = 85,
            }, "c9d4c053-49b6-410c-bc78-2d54a9991871");
            Assert.IsType<UserProfileForReadDto>(result);
            Assert.Equal(176, result.Height);
        }
        [Fact]
        public async void UpdateUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
               .ReturnsAsync(() => null);
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new UserProfileForUpdateDto { });
            Assert.False(result);
        }
        [Fact]
        public async void UpdateUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 175,
                        Weight = 85,
                        UserId = "c9d4c053-49b6-410c-bc78-2d54a9991871"
                    }
                );
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new UserProfileForUpdateDto { });
            Assert.True(result);
        }
        [Fact]
        public async void PartiallyUpdateUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
               .ReturnsAsync(() => null);
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                new Marvin.JsonPatch.JsonPatchDocument<UserProfileForUpdateDto> { });
            Assert.False(result);
        }
        [Fact]
        public async void PartiallyUpdateUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 175,
                        Weight = 85,
                        UserId = "c9d4c053-49b6-410c-bc78-2d54a9991871"
                    }
                );
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                new Marvin.JsonPatch.JsonPatchDocument<UserProfileForUpdateDto> { });
            Assert.True(result);
        }
        [Fact]
        public async void DeleteUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync(() => null);
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.False(result);
        }
        [Fact]
        public async void DeleteUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 175,
                        Weight = 85,
                        UserId = "c9d4c053-49b6-410c-bc78-2d54a9991871"
                    }
                );
            var service = new UserService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteUser(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.True(result);
        }
        private IEnumerable<UserProfile> GetUsers(int num)
        {
            var users = new List<UserProfile>();
            if (num > 0)
            {
                users.Add(
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 175,
                        Weight = 85,
                        UserId = "c9d4c053-49b6-410c-bc78-2d54a9991871"
                    });
            }
            return users;
        }
    }
}
