using currency_converter.Data;
using currency_converter.Data.Entities;
using currency_converter.Data.Models.Dto.ConversionDtos;
using currency_converter.Helpers;
using Microsoft.EntityFrameworkCore;

namespace currency_converter.Services
{
    public class ConverterService
    {
        private readonly ConverterContext _context;
        public ConverterService(ConverterContext context) { _context = context; }
        public IEnumerable<AdminConversionDto> AdminGetAll()
        {
            return _context.Conversions
                .Include(c => c.FromCurrency)
                .Include(c => c.ToCurrency)
                .Select(c => DtoHandler.GetAdminConversionDto(c))
                .ToList();
        }
        public AdminConversionDto? AdminGet(int conversionId)
        {
            Conversion? conversion = _context.Conversions
                .Include(c => c.FromCurrency)
                .Include(c => c.ToCurrency)
                .FirstOrDefault(c => c.Id == conversionId);
            return conversion is null ? null : DtoHandler.GetAdminConversionDto(conversion);
        }
        public IEnumerable<ConversionDto> GetAll(int userId)
        {
            return _context.Conversions
                .Include(c => c.FromCurrency)
                .Include(c => c.ToCurrency)
                .Where(c => c.UserId == userId)
                .Select(c => DtoHandler.GetConversionDto(c))
                .ToList();
        }
        public bool Exists(int conversionId)
        {
            return _context.Conversions.Any(c => c.Id == conversionId);
        }
        public bool CanConvert(int userId)
        {
            User user = _context.Users.Include(u => u.Subscription).First(u => u.Id == userId);
            return user.Subscription.ConverterLimit == -1 || user.ConverterUses < user.Subscription.ConverterLimit;
        }
        public int RemainingConversions(int userId)
        {
            User user = _context.Users.Include(u => u.Subscription).First(u => u.Id == userId);
            return user.Subscription.ConverterLimit == -1 ? -1 : user.Subscription.ConverterLimit - user.ConverterUses;
        }
        public bool ValidCurrency(int currencyId)
        {
            return _context.Currencies.Any(c => c.Id == currencyId);
        }
        public float Convert(ConversionRequestDto dto, int userId)
        {
            Currency fromCurrency = _context.Currencies.First(c => c.Id == dto.FromCurrencyId);
            Currency toCurrency = _context.Currencies.First(c => c.Id == dto.ToCurrencyId);

            float result = Converter.Convert(dto.Amount, fromCurrency, toCurrency);

            User user = _context.Users.Include(u => u.ConversionHistory).First(u => u.Id == userId);
            user.ConverterUses++;

            Conversion conversion = new Conversion()
            {
                UserId = user.Id,
                Date = DateTime.UtcNow,
                FromCurrencyId = fromCurrency.Id,
                FromCurrency = fromCurrency,
                FromCurrencyIndex = fromCurrency.ConversionIndex,
                Amount = dto.Amount,
                ToCurrencyId = toCurrency.Id,
                ToCurrency = toCurrency,
                ToCurrencyIndex = toCurrency.ConversionIndex,
                Result = result
            };

            user.ConversionHistory.Add(conversion);
            _context.Users.Update(user);
            _context.Add(conversion);
            _context.SaveChanges();

            return result;
        }
        public void Delete(int conversionId)
        {
            Conversion? conversion = _context.Conversions.FirstOrDefault(c => c.Id == conversionId);
            if (conversion is null) return;
            _context.Conversions.Remove(conversion);
            _context.SaveChanges();
        }
    }
}
