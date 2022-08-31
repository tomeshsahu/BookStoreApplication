using BusinessLayer.Interface;
using DatabaseLayer.UserPostModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        public IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Registration")]
         public IActionResult Registration(UserRegisterModel userRegistermodel)
        {
            try
            {
                var result = this.userBL.Registration(userRegistermodel);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "User Registration Successfully...!" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "user Registration UnSuccessfull...!" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("UserLogin")]

        public IActionResult UserLogin(UserLogin userLogin)
        {
            try
            {
                var result = this.userBL.Login(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "User Loign Successfully...!",data=result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User Login UnSuccessfull...!" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("ForgetPasswordUser")]
        public IActionResult ForgetPasswordUser(string email)
        {
            try
            {
                var result=this.userBL.ForgetPasswordUser(email);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Forgate password Link send Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Forgate Password UnSuccessFull...!" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(PasswordModel passwordModel)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
            var result=this.userBL.ResetPassoword(email,passwordModel);
            if( result!=null)
            {
                return this.Ok(new { success = true, message = "Reset Password Successfull...!" });
            }
            else
            {
                return this.BadRequest(new { success = true, message = "Password Reset Unsuccessfull..!" });
            }
        }

    }
}
