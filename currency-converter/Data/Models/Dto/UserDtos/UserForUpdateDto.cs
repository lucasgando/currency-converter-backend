namespace currency_converter.Data.Models.Dto.UserDtos
{
    public class UserForUpdateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int SubscriptionId { get; set; } = 0;
    }
}
