using DatabaseLayer.CardModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        private readonly string connetionString;
        public CartRL(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("StoreBook");
        }
        public string AddToCart(int UserId,CartPostModel cartPostModel) 
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spAddCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", cartPostModel.BookId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookQuantity", cartPostModel.BookQuantity);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "successfull";
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

        public string DeleteCart(int UserId, int CartId)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "Deleted";
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateCart(int UserId, UpdatePostModel updatePostModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CartId", updatePostModel.CartId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookQuantity", updatePostModel.BookQuantity);

                connection.Open();
                var result= cmd.ExecuteNonQuery();
                connection.Close();

                if(result!=0)
                {
                    return "Updated";
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



        //GetAllCart
        public List<CartBook> GetAllBooksCart(int UserId)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            List<CartBook> cartmodell = new List<CartBook>();
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAllCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                SqlDataReader rdd = cmd.ExecuteReader();
               while(rdd.Read())
                {
                    CartBook cart = new CartBook();
                  

                    cart.CartId = Convert.ToInt32(rdd["CartId"]);
                    cart.BookId = Convert.ToInt32(rdd["BookId"]);
                    cart.UserId = Convert.ToInt32(rdd["UserId"]);
                    cart.BookQuantity = Convert.ToInt32(rdd["BookQuantity"]);
                    cart.BookName = Convert.ToString(rdd["BookName"]);
                    cart.AuthorName = Convert.ToString(rdd["AuthorName"]);
                    cart.ActualPrice = Convert.ToInt32(rdd["ActualPrice"]);
                    cart.DiscountPrice = Convert.ToInt32(rdd["DiscountedPrice"]);
                    cart.BookImage = Convert.ToString(rdd["BookImage"]);
                    cart.Description = Convert.ToString(rdd["Description"]);

                    cartmodell.Add(cart);
                    
                }
                connection.Close();
                if(cartmodell.Count!=0)
                {
                    return cartmodell; ;
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
    }
}
