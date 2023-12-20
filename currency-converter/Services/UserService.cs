using currency_converter.Data;
using currency_converter.Data.Entities;
using currency_converter.Data.Models.Dto.UserDtos;
using currency_converter.Data.Models.Enums;
using currency_converter.Helpers;
using Microsoft.EntityFrameworkCore;

namespace currency_converter.Services
{
    public class UserService
    {
        private readonly ConverterContext _context;
        private readonly Currency? _usd;
        public UserService(ConverterContext converterContext)
        {
            _context = converterContext;
            _usd = _context.Currencies.FirstOrDefault(c => c.Symbol == "USD");
        }
        public IEnumerable<UserDto> GetAll()
        {
            return _context.Users
                .Include(u => u.Subscription)
                .Include(u => u.FavoriteCurrencies)
                .Select(u => DtoHandler.GetUserDto(u))
                .ToList();
        }
        public UserDto? Get(int id)
        {
            User? user = _context.Users
                .Include(u => u.Subscription)
                .Include(u => u.FavoriteCurrencies)
                .FirstOrDefault(x => x.Id == id);
            return user is null ? null :
                DtoHandler.GetUserDto(user);
        }
        public UserDto? Get(string email)
        {
            User? user = _context.Users
                .Include(u => u.Subscription)
                .Include(u => u.FavoriteCurrencies)
                .SingleOrDefault(user => user.Email == email.ToLower());
            return user is null ? null : DtoHandler.GetUserDto(user);
        }
        public bool Authenticate(string email, string password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user is null ? false : user.PasswordHash == PasswordHasher.GetHash(password);
        }
        public bool Exists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }
        public bool Exists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
        public int Add(UserForCreationDto dto, bool admin)
        {
            User user = new User()
            {
                Username = dto.Username,
                Email = dto.Email.ToLower(),
                PasswordHash = PasswordHasher.GetHash(dto.Password),
                Role = admin ? RoleEnum.Admin : RoleEnum.User,
                SubscriptionId = dto.SubscriptionId,
                FavoriteCurrencies = new List<Currency>(),
                ConversionHistory = new List<Conversion>(),
                ConverterUses = 0
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }
        public void Update(UserForUpdateDto dto)
        {
            User user = _context.Users.Single(u => u.Email == dto.Email.ToLower());
            user.Username = dto.Username;
            user.Email = dto.Email;
            user.PasswordHash = PasswordHasher.GetHash(dto.Password);
            _context.SaveChanges();
        }
        public void Delete(UserForDeletionDto dto)
        {
            User userToDelete = _context.Users.Single(u => u.Email == dto.Email);
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }
}
