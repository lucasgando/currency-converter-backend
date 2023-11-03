using currency_converter.Data.Models.Enums;

namespace currency_converter.Data.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RoleEnum Role { get; set; }
        public int SubscriptionId { get; set; }
    }
}
