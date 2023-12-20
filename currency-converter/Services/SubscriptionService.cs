using currency_converter.Data;
using currency_converter.Data.Entities;
using currency_converter.Data.Models.Dto.SubscriptionDtos;
using currency_converter.Helpers;
using Microsoft.EntityFrameworkCore;

namespace currency_converter.Services
{
    public class SubscriptionService
    {
        private readonly ConverterContext _context;
        public SubscriptionService(ConverterContext converterContext) { _context = converterContext; }
        public IEnumerable<SubscriptionDto> GetAll()
        {
            return _context.Subscriptions.Select(s => DtoHandler.GetSubscriptionDto(s)).ToList();
        }
        public SubscriptionDto? Get(int id)
        {
            Subscription? subscription = _context.Subscriptions.SingleOrDefault(s => s.Id == id);
            return subscription is null ? null : DtoHandler.GetSubscriptionDto(subscription);
        }
        public bool Exists(int id)
        {
            return _context.Subscriptions.Any(s => s.Id == id);
        }
        public void UpdateSubscription(int userId, int subscriptionId)
        {
            User user = _context.Users.First(u => u.Id == userId);
            user.SubscriptionId = subscriptionId;
            _context.SaveChanges();
        }
        public int Add(SubscriptionForCreationDto dto)
        {
            Subscription subscription = new Subscription()
            {
                Name = dto.Name,
                ConverterLimit = dto.ConverterLimit,
                UsdPrice = dto.UsdPrice
            };
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
            return subscription.Id;
        }
        public void Update(SubscriptionForUpdateDto dto)
        {
            Subscription subscription = _context.Subscriptions.First(s => s.Id == dto.Id);
            subscription.Name = dto.Name;
            subscription.ConverterLimit = dto.ConverterLimit;
            subscription.UsdPrice = dto.UsdPrice;
            _context.Update(subscription);
            _context.SaveChanges();
        }
        public void Delete(SubscriptionForDeletionDto dto)
        {
            Subscription subscription = _context.Subscriptions.Include(s => s.Users).First(s => s.Id == dto.Id);
            foreach (User user in subscription.Users)
            {
                user.SubscriptionId = 1;
            }
            _context.Subscriptions.Remove(subscription);
            _context.SaveChanges();
        }
    }
}
