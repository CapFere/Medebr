using Medebr.Data.Entities;
using Medebr.Data.Repositories;
using Medebr.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medebr.Controllers
{
    public class CartController:Controller
    {
        private readonly IProductRepository _repository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;

        public CartController(IProductRepository repository,ShoppingCart shoppingCart, IOrderRepository orderRepository)
        {
            _repository = repository;
            _shoppingCart = shoppingCart;
            _orderRepository = orderRepository;
        }
        [HttpGet("cart")]
        public IActionResult Cart() {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartViewModel = new ShoppingCartViewModel {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }
        [Authorize]
        public IActionResult AddToCart(int id) {
            var selectedProduct = _repository.GetProductsById(id);
            if (selectedProduct!=null) {
                _shoppingCart.AddToCart(selectedProduct,1);
            }
            return RedirectToAction("Cart");
        }
        public IActionResult RemoveFromCart(int id)
        {
            var selectedProduct = _repository.GetProductsById(id);
            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Cart");
        }

        [HttpGet("cart/checkout")]
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost("cart/checkout")]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if (ModelState.IsValid) {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                ModelState.Clear();
            }
            return RedirectToAction("CheckoutComplete"); ;
        }

        [HttpGet("success")]
        public IActionResult CheckoutComplete() {
            ViewBag.Complete = "Order Submitted Successfully, Thanks";
            return View();
        }
        [HttpGet("cart/checkouts")]
        public IActionResult DisplayChekout()
        {
            var checkout = _orderRepository.GetOrders();
            return View(checkout);
        }
    }
}
