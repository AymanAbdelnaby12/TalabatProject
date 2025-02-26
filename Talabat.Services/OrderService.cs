using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Specifications.OrderSpec;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;// this is the unit of work that contain all repositories
        //private readonly IGenericRepository<Product> _productRepository;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepository;  
        //private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
            // IGenericRepository<Product> productRepository,IGenericRepository<DeliveryMethod> deliveryMethodRepository,
            //IGenericRepository<Order> orderRepository)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;

            //_productRepository = productRepository;
            //_deliveryMethodRepository = deliveryMethodRepository;
            //_orderRepository = orderRepository;
        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //1. get basket from the basketrepo
            var Basket =await _basketRepository.GetBasketByIdAsync(BasketId);
            //2. get items from the product repo
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var itemOrdered = new ProductItemOrderd(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(itemOrdered, product.Price, item.Quantity);
                    OrderItems.Add(orderItem);
                }
            }

            //3.calculate subtotal
            var subtotal = OrderItems.Sum(item => item.Price * item.Quantity);
            //4. Get Delivery Method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //5. create order
            var order = new Order(BuyerEmail, ShippingAddress, deliveryMethod, OrderItems, subtotal);
            //6.add order locally
            await _unitOfWork.Repository<Order>().AddAsync(order);
            //7. save to db
         var result= await _unitOfWork.completeAsync();
            if(result <= 0) return null;
            //8. return order
            return order; 
            
        }

        public async Task<IReadOnlyList<Order>> CreateOrderByIdForSpecificUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpec(BuyerEmail); 
            var order=await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return order;

        }

        public async Task<Order?> GetOrderById(int OrderId, string BuyerEmail)
        {
            var spec = new OrderSpec(BuyerEmail, OrderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpcAsync(spec);
            return order;
        }
    }
}
