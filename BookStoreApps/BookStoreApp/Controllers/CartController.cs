using BusinessLayer.Interface;
using DatabaseLayer.CardModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [Controller]
    [Route("Controller")]
    public class CartController : ControllerBase
    {
        ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CartPostModel cartPostModel)
        {
            try
            {
               
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                var result = this.cartBL.AddToCard(UserId, cartPostModel);
                if(result!=null)
                {
                    return this.Ok(new { success = true, Message = "Book Added To Cart Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book Not Added To Cart" });

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteCart")]
        public IActionResult DeleteCart(int CartId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                var result= this.cartBL.DeleteCart(UserId, CartId);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Book Delete Successfully From Cart" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book Not Deleted " });

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [HttpPut("UpdateCart")]
        public IActionResult UpdateCart(UpdatePostModel updatePostModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                var result = this.cartBL.UpdateCart(UserId, updatePostModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Book Updated Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book Not updated " });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllBooksCart")]
        public IActionResult GetAllBooksCart()
        {
            List<CartBook> carts = new List<CartBook>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                carts = this.cartBL.GetAllBooksCart(UserId);

                if (carts != null)
                {
                    return this.Ok(new { success = true, Message = "Cart Get All Successfully",data=carts });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Cart Get All Unsuccessfully " });

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
