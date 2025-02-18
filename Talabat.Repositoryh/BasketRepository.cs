using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        //ask CLR to inject the redis connection and ask ClR for object from class that implement IConnectionMultiplexer
        public BasketRepository(IConnectionMultiplexer redis)    {
            _database = redis.GetDatabase();  
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
          return  await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketByIdAsync(string basketId)
        {
            var data = _database.StringGet(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var created = _database.StringSet(basket.Id, jsonBasket, TimeSpan.FromDays(1));
            if (!created) return null;
            return GetBasketByIdAsync(basket.Id);
        }
    }
}
