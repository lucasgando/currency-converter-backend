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
        public DbSet<Conversion> Conversions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User admin = new User()
            {
                Id = 1,
                Username = "Admin",
                Email = "admin@mail.com",
                PasswordHash = PasswordHasher.GetHash("admin"),
                Role = RoleEnum.Admin,
                SubscriptionId = 2,
                ConverterUses = 0,
                ConversionHistory = new List<Conversion>(),
                FavoriteCurrencies = new List<Currency>()
            };
            User testUser = new User()
            {
                Id = 2,
                Username = "user1",
                Email = "user@mail.com",
                PasswordHash = PasswordHasher.GetHash("password"),
                Role = RoleEnum.User,
                SubscriptionId = 1,
                ConverterUses = 0,
                ConversionHistory = new List<Conversion>(),
                FavoriteCurrencies = new List<Currency>()
            };
            Subscription free = new Subscription()
            {
                Id = 1,
                Name = "Free",
                ConverterLimit = 10,
                UsdPrice = 0F,
                SubscriptionPicture = "http://2.bp.blogspot.com/-Kjm_y-4VY6Q/T-CZuH4eDMI/AAAAAAAABUg/H5mIxef6Tjc/s1600/Free1.jpg"
            };
            Subscription premium = new Subscription()
            {
                Id = 2,
                Name = "Premium",
                ConverterLimit = -1,
                UsdPrice = 9.99F,
                SubscriptionPicture = ""
            };
            Subscription trial = new Subscription()
            {
                Id = 3,
                Name = "Trial",
                ConverterLimit = 100,
                UsdPrice = 2.49F,
                SubscriptionPicture = ""
            };
            modelBuilder.Entity<User>(u =>
            {
                u.HasOne(u => u.Subscription)
                    .WithMany(c => c.Users);
                u.HasMany(u => u.ConversionHistory)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId);
                u.HasMany(u => u.FavoriteCurrencies)
                    .WithMany();
                u.HasData(admin, testUser);
            });
            modelBuilder.Entity<Subscription>(s => 
            { 
                s.HasMany(s => s.Users)
                    .WithOne(u => u.Subscription);
                s.HasData(free, premium, trial);
            });
            modelBuilder.Entity<Conversion>(c =>
            {
                c.HasOne(c => c.User)
                    .WithMany(u => u.ConversionHistory)
                    .HasForeignKey(c => c.UserId);
                c.HasOne(c => c.FromCurrency)
                    .WithMany()
                    .HasForeignKey(c => c.FromCurrencyId);
                c.HasOne(c => c.ToCurrency)
                    .WithMany()
                    .HasForeignKey(c => c.ToCurrencyId);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
