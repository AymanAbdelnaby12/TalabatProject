using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Services;
using Product = Talabat.Core.Models.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration? _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IConfiguration? configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey=_configuration["StripeSettings:SecretKey"];

            //get the basket with the items
            var Basket = await _basketRepository.GetBasketByIdAsync(BasketId);
            if (Basket.Items == null) return null;

            // Amout to be paid = subtotal + deliveryMethodCost
            var shippingPrice = 0M;
            if (Basket.DeliveryMethodId.HasValue) 
            { 
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;   
            }

            if(Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if (product .Price != item.Price)
                        item.Price = product.Price;
                    
                }
                var subtotal = Basket.Items.Sum(item => item.Price * item.Quantity);
                var service= new PaymentIntentService();
                PaymentIntent paymentIntent;
                if (string.IsNullOrEmpty(Basket.PaymentIntentId)) // Create payment
                {
                    var options = new PaymentIntentCreateOptions()
                    {
                        Amount = (long)subtotal * 100 + (long)shippingPrice * 100,
                        Currency = "usd",
                        PaymentMethodTypes = new List<string> { "card" }
                    };
                    paymentIntent = await service.CreateAsync(options);
                    Basket.PaymentIntentId = paymentIntent.Id;
                    Basket.ClientSecret = paymentIntent.ClientSecret;

                }
                else // update payment
                { 
                    var options = new PaymentIntentUpdateOptions()
                    {
                        Amount = (long)subtotal * 100 + (long)shippingPrice * 100
                    };
                 paymentIntent = await service.UpdateAsync(Basket.PaymentIntentId, options);
                    Basket.PaymentIntentId = paymentIntent.Id;
                    Basket.ClientSecret = paymentIntent.ClientSecret;
                }
            }
            await _basketRepository.UpdateBasketAsync(Basket);
            return Basket;
        }
    }
}
