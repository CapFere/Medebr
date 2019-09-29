using Medebr.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MedebrContext _ctx;
        private readonly ShoppingCart _shoppingCart;
        private readonly IProductRepository _repository;

        public OrderRepository(MedebrContext ctx, ShoppingCart shoppingCart,IProductRepository repository)
        {
            _ctx = ctx;
            _shoppingCart = shoppingCart;
            _repository = repository;
        }

        public void CreateOrder(Order order) {
            order.OrderPlaced = DateTime.Now;
            _ctx.Orders.Add(order);
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.ProductId,
                    OrderId = order.OrderId,
                    Price = item.Product.Price
                };
                _ctx.OrderDetails.Add(orderDetail);
            }
            _ctx.SaveChanges();
        }

        public List<Checkout> GetOrders()
        {
            var orderItem = _ctx.OrderDetails.ToList();
            var order = _ctx.Orders.ToList();
            List<Checkout> chceckouts = new List<Checkout>();
            foreach (var item in order)
            {
                Checkout chceckout = new Checkout();
                chceckout.Order = item;
                foreach (var i in orderItem)
                {
                    if (i.OrderId == item.OrderId) {
                        Product product = _repository.GetProductsById(i.ProductId);
                        chceckout.Products.Add(product);
                    }
                }
                chceckouts.Add(chceckout);
            }
            

            return chceckouts;
        }
    }
}
