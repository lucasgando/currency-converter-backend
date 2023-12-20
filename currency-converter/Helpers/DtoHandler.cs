using currency_converter.Data.Entities;
using currency_converter.Data.Models.Dto.ConversionDtos;
using currency_converter.Data.Models.Dto.CurrencyDtos;
using currency_converter.Data.Models.Dto.SubscriptionDtos;
using currency_converter.Data.Models.Dto.UserDtos;

namespace currency_converter.Helpers
{
    public static class DtoHandler
    {
        public static UserDto GetUserDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Subscription = GetSubscriptionDto(user.Subscription),
                FavoriteCurrencies = user.FavoriteCurrencies.Select(c => GetCurrencyDto(c)).ToList()
            };
        }
        public static CurrencyDto GetCurrencyDto(Currency currency)
        {
            return new CurrencyDto()
            {
                Id = currency.Id,
                Name = currency.Name,
                Symbol = currency.Symbol,
                ConversionIndex = currency.ConversionIndex,
                Flag = currency.Flag
            };
        }
        public static SubscriptionDto GetSubscriptionDto(Subscription subscription)
        {
            return new SubscriptionDto()
            {
                Id = subscription.Id,
                Name = subscription.Name,
                ConverterLimit = subscription.ConverterLimit,
                UsdPrice = subscription.UsdPrice,
                SubscriptionPicture = subscription.SubscriptionPicture
            };
        }
        public static ConversionDto GetConversionDto(Conversion conversion)
        {
            return new ConversionDto()
            {
                Id = conversion.Id,
                Date = conversion.Date,
                Amount = conversion.Amount,
                FromCurrency = GetCurrencyDto(conversion.FromCurrency),
                FromCurrencyIndex = conversion.FromCurrencyIndex,
                ToCurrency = GetCurrencyDto(conversion.ToCurrency),
                ToCurrencyIndex = conversion.ToCurrencyIndex,
                Result = conversion.Result
            };
        }
        public static AdminConversionDto GetAdminConversionDto(Conversion conversion)
        {
            return new AdminConversionDto()
            {
                Id = conversion.Id,
                UserId = conversion.UserId,
                Date = conversion.Date,
                Amount = conversion.Amount,
                FromCurrency = GetCurrencyDto(conversion.FromCurrency),
                FromCurrencyIndex = conversion.FromCurrencyIndex,
                ToCurrency = GetCurrencyDto(conversion.ToCurrency),
                ToCurrencyIndex = conversion.ToCurrencyIndex,
                Result = conversion.Result
            };
        }
    }
}
