namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class UserForReadDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserProfileForReadDto UserProfile { get; set; }
    }
}
