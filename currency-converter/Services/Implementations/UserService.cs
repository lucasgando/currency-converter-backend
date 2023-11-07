using currency_converter.Data;
using currency_converter.Data.Entities;
using currency_converter.Data.Models.Dto;
using currency_converter.Data.Models.Enums;
using currency_converter.Helpers;

namespace currency_converter.Services.Implementations
{
    public class UserService
    {
        private readonly ConverterContext _context;
        public UserService(ConverterContext converterContext) { _context = converterContext; }
        public IEnumerable<UserDto> GetAll()
        {
            return _context.Users.Select(x => new UserDto 
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
                PasswordHash = x.PasswordHash,
                SubscriptionId = x.SubscriptionId,
            }).ToList();
        }
        public UserDto? GetById(int id)
        {
            User? user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user is null ? null :
                new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    SubscriptionId = user.SubscriptionId,
                };
        }
        public UserDto? GetByEmail(string email)
        {
            User? user = _context.Users.SingleOrDefault(user => user.Email == email.ToLower());
            return user is null ? null : new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                SubscriptionId = user.SubscriptionId
            };
        }
        public bool Authenticate(string email, string password)
        {
            UserDto? user = GetByEmail(email.ToLower());
            if (user == null) return false;
            return user.PasswordHash == PasswordHasher.GetHash(password);
        }
        public bool Exists(string emailOrUsername)
        {
            return _context.Users.Any(x => x.Email == emailOrUsername || x.Username == emailOrUsername);
        }
        public bool Exists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }
        public void Add(UserForCreationDto dto, bool admin)
        {
            User user = new User()
            {
                Username = dto.Username,
                Email = dto.Email.ToLower(),
                PasswordHash = PasswordHasher.GetHash(dto.Password),
                Role = (admin ? RoleEnum.Admin : RoleEnum.User),
                SubscriptionId = 1
            };
            _context.Users.Add(user);
            _context.SaveChanges();
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
