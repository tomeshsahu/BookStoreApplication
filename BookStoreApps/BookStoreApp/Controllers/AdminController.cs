using DatabaseLayer.Admin;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class AdminController : ControllerBase
    {
        IAdminRL adminRL;

        public AdminController(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        [HttpPost("AdminLogin")]
         public IActionResult AdminLogin(AdminLoginModel adminLoginModel )
        {
            try
            {
                var result=this.adminRL.AdminLogin(adminLoginModel);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Admin Login Successfully", token = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Admin Login Unsuccessfully" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
