using DatabaseLayer.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly string connetionString;
        public AdminRL(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("StoreBook");
        }
        public string AdminLogin(AdminLoginModel adminLoginModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
       
            try
            {
                SqlCommand cmd = new SqlCommand("spAdminLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", adminLoginModel.Email);
                cmd.Parameters.AddWithValue("@Password", adminLoginModel.Password);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                SqlDataReader redd = cmd.ExecuteReader();
                AdminLoginModel adminLogin = new AdminLoginModel();
                if(redd.Read())
                {
                    
                    adminLogin.Email = redd["Email"] == DBNull.Value ? default : redd.GetString("Email");
                    adminLogin.Password = redd["password"] == DBNull.Value ? default : redd.GetString("Password");
                }
                connection.Close();

                if(adminLogin.Email==adminLoginModel.Email)
                {
                    return GenerateToken(adminLogin.Email);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        private string GenerateToken(string email)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email)

                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
