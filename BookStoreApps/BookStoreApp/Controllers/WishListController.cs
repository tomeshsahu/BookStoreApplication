using BusinessLayer.Interface;
using DatabaseLayer.WishListModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class WishListController : Controller
    {
        IWishlistBL wishListBL;
        public WishListController(IWishlistBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [HttpPost("AddToWishList")]
        public IActionResult AddToWishList(WishlistPostModel wishlistPostModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                var result = this.wishListBL.AddToWishList(UserId, wishlistPostModel);

         
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Wishlist Added Successfully...!" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Wishlist Not Added...!" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteWishList")]
        public IActionResult DeleteWishList(int WishListId)
        {

            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                var result = this.wishListBL.DeleteWishList(UserId, WishListId);


                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Wishlist Deleted Successfully...!" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Wishlist Not Deleted...!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("GetWishlistByUserid")]
        public IActionResult GetWishlistByUserid()
        {

            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                var result = this.wishListBL.GetWishlistByUserid(UserId);


                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Get All Wishlist Successfully...!",data=result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Wishlist Data Not Found...!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
