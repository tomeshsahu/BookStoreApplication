using DatabaseLayer.UserPostModel;
using Experimental.System.Messaging;
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
    public class UserRL : IUserRL
    {
        private readonly string connetionString;
        public UserRL(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("StoreBook");
        }
        public UserRegisterModel Registration(UserRegisterModel userRegistrationModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                var Encrypted= EncryptPassword(userRegistrationModel.Password);

                SqlCommand cmd = new SqlCommand("spAddUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FullName", userRegistrationModel.FullName);
                cmd.Parameters.AddWithValue("@Email", userRegistrationModel.Email);
                cmd.Parameters.AddWithValue("@Password", Encrypted);
                cmd.Parameters.AddWithValue("@Mobile", userRegistrationModel.Mobile);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if(result != 0)
                {
                    return userRegistrationModel;
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

        public static string EncryptPassword(string password)
        {
            try
            {
                if (password == null)
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Login(UserLogin userLogin)
        {
            SqlConnection connection=new SqlConnection(connetionString);
            try
            {
                string DBPasswrod = "";
                int UserId = 0;
              
                SqlCommand cmd = new SqlCommand("spLoginUser",connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", userLogin.Email);
                connection.Open();
                var res=cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd.ExecuteReader();
            
                while (rd.Read())
                {
                     DBPasswrod = Convert.ToString(rd["Password"]);
                     UserId = Convert.ToInt32(rd["UserId"]);
                }
                connection.Close();
                var Decript = DecryptedPassword(DBPasswrod);
                if(Decript==userLogin.Password)
                {
                    if(Decript!=null)
                    {
                        var token= this.GenerateJWTTokennnn(userLogin.Email, UserId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
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



        //-----------------
        private string GenerateJWTTokennnn(string Email, int UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("Email", Email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),// valid for 2 hr

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //-----------

        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encryptedPassword == null)
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool ForgetPasswordUser(string email)
        {
            SqlConnection connection = new SqlConnection(connetionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spForgetPasswordUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Email", email);
                    var result = com.ExecuteNonQuery();
                    SqlDataReader rd = com.ExecuteReader();
                    ForgateUser response = new ForgateUser();
                    if (rd.Read())
                    {
                        response.UserId = rd["UserId"] == DBNull.Value ? default : rd.GetInt32("UserId");
                        response.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                        response.FullName = rd["FullName"] == DBNull.Value ? default : rd.GetString("FullName");


                    }
                    MessageQueue messageQueue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                    {
                        messageQueue = new MessageQueue(@".\Private$\FundooQueue");
                    }
                    else
                    {
                        messageQueue = MessageQueue.Create(@".\Private$\FundooQueue");
                    }
                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJWTToken(email, response.UserId);
                    MyMessage.Label = "Forget Password Email";
                    messageQueue.Send(MyMessage);
                    Message msg = messageQueue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(email, msg.Body.ToString(), response.FullName);
                    messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    messageQueue.BeginReceive();
                    messageQueue.Close();
                    return true;


                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object GenerateJWTToken(string email, int userId)
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
                    new Claim("Email", email),
                    new Claim("UserId",userId.ToString())
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

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
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

        public bool ResetPassword(string email, PasswordModel passwordModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            var result = 0;

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spResetPassword", connection);
                    com.CommandType = CommandType.StoredProcedure;
                   
                    if (passwordModel.Password == passwordModel.ConfirmPassword)
                    {
                        var ResetPasswordEncript = EncryptPassword(passwordModel.Password);
                        com.Parameters.AddWithValue("@Email", email);
                        com.Parameters.AddWithValue("@Password", ResetPasswordEncript);

                    }
                    result = com.ExecuteNonQuery();

                    if (result > 0)
                        return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
    }
}
