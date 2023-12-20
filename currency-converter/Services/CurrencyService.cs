using currency_converter.Data;
using currency_converter.Data.Entities;
using currency_converter.Data.Models.Dto.CurrencyDtos;
using currency_converter.Helpers;
using Microsoft.EntityFrameworkCore;

namespace currency_converter.Services
{
    public class CurrencyService
    {
        private readonly ConverterContext _context;
        public CurrencyService(ConverterContext context) { _context = context; }
        public IEnumerable<CurrencyDto> GetAll()
        {
            return _context.Currencies.Select(c => DtoHandler.GetCurrencyDto(c)).ToList();
        }
        public CurrencyDto? Get(int id)
        {
            Currency? currency = _context.Currencies.FirstOrDefault(c => c.Id == id);
            return currency is null ? null : DtoHandler.GetCurrencyDto(currency);
        }
        public IEnumerable<CurrencyDto> GetFav(int userId)
        {
            User user = _context.Users.Include(u => u.FavoriteCurrencies).First(u => u.Id == userId);
            return user.FavoriteCurrencies.Select(c => DtoHandler.GetCurrencyDto(c)).ToList();
        }
        public void AddFav(int userId, int currencyId)
        {
            Currency currency = _context.Currencies.First(c => c.Id == currencyId);
            User user = _context.Users.Include(u => u.FavoriteCurrencies).First(u => u.Id == userId);
            if (!user.FavoriteCurrencies.Any(c => c.Id == currency.Id))
            {
                user.FavoriteCurrencies.Add(currency);
                _context.SaveChanges();
            }
        }
        public void RemoveFav(int userId, int currencyId)
        {
            Currency currency = _context.Currencies.First(c => c.Id == currencyId);
            User user = _context.Users.Include(u => u.FavoriteCurrencies).First(u => u.Id == userId);
            if (user.FavoriteCurrencies.Any(c => c.Id == currency.Id))
            {
                user.FavoriteCurrencies.Remove(currency);
                _context.SaveChanges();
            }

        }
        public bool Exists(int id)
        {
            return _context.Currencies.Any(c => c.Id == id);
        }
        public bool Exists(string name, string symbol)
        {
            return _context.Currencies.Any(c => c.Name == name || c.Symbol == symbol);
        }
        public int Add(CurrencyForCreationDto dto)
        {
            Currency currency = new Currency()
            {
                Name = dto.Name,
                Symbol = dto.Symbol,
                ConversionIndex = dto.ConversionIndex,
                Flag = dto.Flag
            };
            _context.Currencies.Add(currency);
            _context.SaveChanges();
            return currency.Id;
        }
        public void Update(CurrencyForUpdateDto dto)
        {
            Currency currency = _context.Currencies.Single(u => u.Id == dto.Id);
            currency.Name = dto.Name;
            currency.Symbol = dto.Symbol;
            currency.ConversionIndex = dto.ConversionIndex;
            currency.Flag = dto.Flag;
            _context.Currencies.Update(currency);
            _context.SaveChanges();
        }
        public void Remove(CurrencyForDeletionDto dto)
        {
            Currency currency = _context.Currencies.First(c => c.Id == dto.Id);
            _context.Currencies.Remove(currency);
            _context.SaveChanges();
        }
    }
}
