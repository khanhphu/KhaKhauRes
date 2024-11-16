﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhaKhau.Controllers
{
    [Authorize]
    public class CartController :Controller
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository ) 
        {
            _cartRepository = cartRepository;
        }
        public async Task<IActionResult> AddItem(int  productId, int qty = 1, int redirect=0)
        {
            var cartCount = await _cartRepository.AddItem(productId, qty);
            if (redirect == 0)
            {
                return Ok(cartCount);
            }
            return RedirectToAction("GetUserCart");
        } 
        public async Task<IActionResult> RemoveItem(int  productId)
        {
           var cartCount=await _cartRepository.RemoveItem(productId);
            return RedirectToAction("GetUserCart");
        }public async Task<IActionResult> GetUserCart()
        {
           var cart=await _cartRepository.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepository.GetCartItemCount();
			return Ok(cartItem);
		}
		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CheckOut(CheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool isCheckedOut = await _cartRepository.DoCheckout(model);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }
        //hai view render if order thanh cong!!
        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }

    }
}
