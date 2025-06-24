namespace HotelListing.API.Models.Users
{
    public class ApplicationUserDto : LoginDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
