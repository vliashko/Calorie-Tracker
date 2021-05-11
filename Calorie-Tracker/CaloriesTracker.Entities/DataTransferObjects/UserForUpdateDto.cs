namespace CaloriesTracker.Entities.DataTransferObjects
{
    public class UserForUpdateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserProfileForUpdateDto UserProfile { get; set; }
    }
}
