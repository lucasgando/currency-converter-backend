using currency_converter.Data.Entities;
using currency_converter.Data.Models.Enums;
using currency_converter.Helpers;
using Microsoft.EntityFrameworkCore;

namespace currency_converter.Data
{
    public class ConverterContext : DbContext
    {
        public ConverterContext(DbContextOptions<ConverterContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User admin = new User()
            {
                Id = 1,
                Username = "Admin",
                Email = "admin@mail.com",
                PasswordHash = PasswordHasher.GetHash("admin"),
                Role = RoleEnum.Admin,
                SubscriptionId = 2
            };
            User testUser = new User()
            {
                Id = 2,
                Username = "user1",
                Email = "user@mail.com",
                PasswordHash = PasswordHasher.GetHash("password"),
                Role = RoleEnum.User,
                SubscriptionId = 1
            };
            Subscription free = new Subscription()
            {
                Id = 1,
                Name = "free",
                ConverterLimit = 10,
                UsdPrice = 0
            };
            Subscription premium = new Subscription()
            {
                Id = 2,
                Name = "premium",
                ConverterLimit = -1,
                UsdPrice = 10
            };
            modelBuilder.Entity<User>(u =>
            {
                u.HasOne(u => u.Subscription)
                    .WithMany(c => c.Users);
                u.HasMany(u => u.FavoriteCurrencies);
                u.HasData(admin, testUser);
            });
            modelBuilder.Entity<Subscription>(s => 
            { 
                s.HasMany(s => s.Users)
                    .WithOne(u => u.Subscription);
                s.HasData(free, premium);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
