using DatabaseLayer.WishListModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL:IWishListRL
    {
        private readonly string connetionString;
        public WishListRL(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("StoreBook");
        }

        public string AddToWishList(int UserId, WishlistPostModel wishlistPostModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spAddWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId", wishlistPostModel.BookId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if(result != 0)
                {
                    return "Added";
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

        public string DeleteWishList(int UserId, int WishListId)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@WishListId", WishListId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if(result!=0)
                {
                    return "Deleted";
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

        public List<WishListResponseModel> GetWishlistByUserid(int UserId)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            List<WishListResponseModel> wishlistres = new List<WishListResponseModel>();
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAllWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                SqlDataReader rerd = cmd.ExecuteReader ();
                while(rerd.Read ())
                {
                    WishListResponseModel model = new WishListResponseModel ();
                    model.WishListId = Convert.ToInt32(rerd["WishListId"]);
                    model.UserId = Convert.ToInt32(rerd["UserId"]);
                    model.BookId = Convert.ToInt32(rerd["BookId"]);
                    model.BookQuantity = Convert.ToInt32(rerd["Quantity"]);
                    model.BookName = Convert.ToString(rerd["BookName"]);
                    model.AuthorName = Convert.ToString(rerd["AuthorName"]);
                    model.ActualPrice = Convert.ToInt32(rerd["ActualPrice"]);
                    model.DiscountedPrice = Convert.ToInt32(rerd["DiscountedPrice"]);
                    model.BookImage = Convert.ToString(rerd["BookImage"]);
                    model.Description = Convert.ToString(rerd["Description"]);

                    wishlistres.Add(model);
                }
                connection.Close();
                
                if(wishlistres.Count>0)
                {
                    return wishlistres;
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
