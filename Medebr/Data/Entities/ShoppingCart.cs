using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Data.Entities
{
    public class ShoppingCart
    {
        private readonly MedebrContext _ctx;

        public ShoppingCart(MedebrContext ctx)
        {
            _ctx = ctx;
        }

        public string ShopingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider service) {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = service.GetService<MedebrContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId",cartId);
            return new ShoppingCart(context) { ShopingCartId = cartId};

        }
        public void AddToCart(Product product, int amount) {
            var shopingCartItem = _ctx.ShopingCartItems.SingleOrDefault(
                s=>s.Product.ProductId == product.ProductId && s.ShopingCartId == ShopingCartId);
            if (shopingCartItem == null)
            {
                shopingCartItem = new ShoppingCartItem
                {
                    ShopingCartId = ShopingCartId,
                    Product = product,
                    Amount = amount

                };
                _ctx.ShopingCartItems.Add(shopingCartItem);
            }
            else {
                shopingCartItem.Amount++;
            }
            _ctx.SaveChanges();
        }

        public int RemoveFromCart(Product product) {
            var shopingCartItem = _ctx.ShopingCartItems.SingleOrDefault(
               s => s.Product.ProductId == product.ProductId && s.ShopingCartId == ShopingCartId);
            var localAmount = 0;
            if (shopingCartItem != null) {
                if (shopingCartItem.Amount>1) {
                    shopingCartItem.Amount--;
                    localAmount = shopingCartItem.Amount;
                }
                else {
                    _ctx.ShopingCartItems.Remove(shopingCartItem);
                }
            }

            _ctx.SaveChanges();
            return localAmount;

        }

        public List<ShoppingCartItem> GetShoppingCartItems() {
            return ShoppingCartItems ??
                (ShoppingCartItems = _ctx.ShopingCartItems.Where(c => c.ShopingCartId == ShopingCartId)
                    .Include(s => s.Product)
                    .ToList());
        }
        public void ClearCart() {
            var cartItems = _ctx
                .ShopingCartItems
                .Where(cart => cart.ShopingCartId == ShopingCartId);
            _ctx.ShopingCartItems.RemoveRange(cartItems);
            _ctx.SaveChanges();
        }
        public decimal GetShoppingCartTotal() {
            var total = _ctx.ShopingCartItems.Where(c => c.ShopingCartId == ShopingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
