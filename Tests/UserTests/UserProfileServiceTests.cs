using AutoMapper;
using Moq;
using System;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;
using UserMicroService.Models;
using UserMicroService.Services;
using Xunit;

namespace UserTests
{
    public class UserProfileServiceTests : IDisposable
    {
        private readonly string userId = "7c2a51b6-ffd3-4f82-8e21-92ca4053a37e";

        Mock<IUserProfileRepository> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public UserProfileServiceTests()
        {
            mockRepo = new Mock<IUserProfileRepository>();
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
        public async void GetUserProfileByUserId_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetUserProfileByUserIdAsync(userId, false)).ReturnsAsync(
                new UserProfile 
                { 
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    DateOfBirth = new DateTime(2021, 6, 9),
                    Calories = 0,
                    Gender = Gender.Male,
                    Height = 175,
                    Weight = 84,
                    UserId = userId
                }
            );
            var service = new UserProfileService(mockRepo.Object, mapper);
            var result = await service.GetUserProfileByUserIdAsync(userId);
            Assert.IsType<UserProfileForReadDto>(result);
            Assert.Equal(175, result.Height);
        }

        [Fact]
        public async void GetUserProfileByUserId_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetUserProfileByUserIdAsync(userId, false))
                .ReturnsAsync(() => null);
            var service = new UserProfileService(mockRepo.Object, mapper);
            var result = await service.GetUserProfileByUserIdAsync(userId);
            Assert.Null(result);
        }

        [Fact]
        public async void CreateUserProfileForUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {    
            var service = new UserProfileService(mockRepo.Object, mapper);
            var result = await service.CreateUserProfileForUserAsync(userId, new UserProfileForCreateDto
            {
                Gender = Gender.Male,
                Height = 175,
                Weight = 84,
                DateOfBirth = new DateTime(2021, 6, 9)
            });
            Assert.IsType<UserProfileForReadDto>(result);
            Assert.Equal(175, result.Height);
        }

        [Fact]
        public async void UpdateUserProfile_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new UserProfileService(mockRepo.Object, mapper);
            var result = await service.UpdateUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new UserProfileForUpdateDto { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void UpdateUserProfile_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = new DateTime(2021, 6, 9),
                        Calories = 0,
                        Gender = Gender.Male,
                        Height = 175,
                        Weight = 84,
                        UserId = userId
                    }
                );
            var service = new UserProfileService(mockRepo.Object, mapper);
            var result = await service.UpdateUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new UserProfileForUpdateDto { });
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateUserProfile_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new UserProfileService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<UserProfileForUpdateDto> { });
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void PartiallyUpdateUserProfile_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(
                    new UserProfile
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        DateOfBirth = new DateTime(2021, 6, 9),
                        Calories = 0,
                        Gender = Gender.Male,
                        Height = 175,
                        Weight = 84,
                        UserId = userId
                    }
                );
            var service = new UserProfileService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateUserProfileAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<UserProfileForUpdateDto> { });
            Assert.Equal(204, result.StatusCode);
        }
    }
}
