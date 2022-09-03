using BusinessLayer.Interface;
using DatabaseLayer.AddressModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class AddressController : Controller
    {
        IAddressBL AddressBL;

        public AddressController(IAddressBL addressBL)
        {
            AddressBL = addressBL;
        }

        [HttpPost("AddAddress")]
         public IActionResult AddAddress(AddressPostModel addressPostModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);

                var result = this.AddressBL.AddAddress(UserId, addressPostModel);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Address Added Successfully",  });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Address Not Added  " });

                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [HttpDelete("DeleteAddress")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);

                var result = this.AddressBL.DeleteAddress(UserId, AddressId);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Address Deleted Successfully", });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Address Not Deleted  " });

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(AddressPostModel addressModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);

                var result = this.AddressBL.UpdateAddress(UserId, addressModel);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Address Updated Successfully", });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Address Not Updated  " });

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("GetAddress")]
        public IActionResult GetAddress(int AddressId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);

                var result = this.AddressBL.GetAddress(UserId, AddressId);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Address Get Successfully",data=result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Address Not Found  " });

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
