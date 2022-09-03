using DatabaseLayer.AddressModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private readonly string connetionString;
        public AddressRL(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("StoreBook");
        }
        public string AddAddress(int UserId, AddressPostModel addressPostModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("SPAddAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Address", addressPostModel.Address);
                cmd.Parameters.AddWithValue("@City",addressPostModel.City);
                cmd.Parameters.AddWithValue("@State", addressPostModel.State);
                cmd.Parameters.AddWithValue("@TypeId", addressPostModel.TypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                var result=cmd.ExecuteNonQuery();
                connection.Close();

                if(result!=0)
                {
                    return "Added Successfully";
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

        public string DeleteAddress(int UserId,int AddressId)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AddressId",AddressId);

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

        public AddressPostModel GetAddress(int UserId, int AddressId)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId",UserId);
                cmd.Parameters.AddWithValue("@AddressId", AddressId);

                AddressPostModel postModel = new AddressPostModel();
                connection.Open();
                SqlDataReader rere = cmd.ExecuteReader();
                while(rere.Read())
                {
                    
                    postModel.AddressId = Convert.ToInt32(rere["AddressId"]);
                    postModel.Address = Convert.ToString(rere["Address"]);
                    postModel.City = Convert.ToString(rere["City"]);
                    postModel.State = Convert.ToString(rere["State"]);
                    postModel.TypeId = Convert.ToInt32(rere["TypeId"]);
                   
                }
                connection.Close();

                if(postModel!=null)
                {
                    return postModel;
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

        public string UpdateAddress(int UserId, AddressPostModel addressPostModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddressId", addressPostModel.AddressId);
                cmd.Parameters.AddWithValue("@City",addressPostModel.City);
                cmd.Parameters.AddWithValue("@State", addressPostModel.State);
                cmd.Parameters.AddWithValue("@Address", addressPostModel.Address);
                cmd.Parameters.AddWithValue("@TypeId", addressPostModel.TypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
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
    }
}
