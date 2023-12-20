using currency_converter.Data.Models.Dto.CurrencyDtos;
using currency_converter.Data.Models.Dto.SubscriptionDtos;
using currency_converter.Data.Models.Enums;

namespace currency_converter.Data.Models.Dto.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public SubscriptionDto Subscription { get; set; }
        public List<CurrencyDto> FavoriteCurrencies { get; set; }
    }
}
