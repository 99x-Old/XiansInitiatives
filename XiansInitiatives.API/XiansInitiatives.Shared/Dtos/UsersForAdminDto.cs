namespace XiansInitiatives.Shared.Dtos
{
    public class UsersForAdminDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
        public bool Locked { get; set; }
    }
}