using DatabaseLayer.BookModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly string connetionString;
        public BookRL(IConfiguration configuration)
        {
            connetionString = configuration.GetConnectionString("StoreBook");
        }
        public BookPostModel AddBook(BookPostModel bookPostModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd=new SqlCommand("spAddBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookName", bookPostModel.BookName);
                cmd.Parameters.AddWithValue("@Description", bookPostModel.Description);
                cmd.Parameters.AddWithValue("@AuthorName", bookPostModel.AuthorName);
                cmd.Parameters.AddWithValue("@ActualPrice", bookPostModel.ActualPrice);
                cmd.Parameters.AddWithValue("@DiscountedPrice", bookPostModel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@Quantity", bookPostModel.Quantity);
                cmd.Parameters.AddWithValue("@BookImage", bookPostModel.BookImage);

                connection.Open();
                var result=  cmd.ExecuteNonQuery();
                connection.Close();

                if(result != 0)
                {
                     return bookPostModel;
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

    

        public List<BookPostModel> GetAllBooks()
        {
            List<BookPostModel> bookPostModels = new List<BookPostModel>();
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAllBooks", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BookPostModel getBook = new BookPostModel();

                    getBook.BookId = Convert.ToInt32(rd["BookId"]);
                    getBook.BookName = Convert.ToString(rd["BookName"]);
                    getBook.AuthorName = Convert.ToString(rd["AuthorName"]);
                    getBook.Description = Convert.ToString(rd["Description"]);
                    getBook.ActualPrice = Convert.ToDouble(rd["ActualPrice"]);
                    getBook.DiscountedPrice = Convert.ToDouble(rd["DiscountedPrice"]);
                    getBook.Quantity = Convert.ToInt32(rd["Quantity"]);
                    getBook.BookImage = Convert.ToString(rd["BookImage"]);
                   bookPostModels.Add(getBook);
                }
                connection.Close();
                if (bookPostModels.Count != 0)
                {
                    return bookPostModels;
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

        public int  DeleteBook(int BookId)
        {
            var result = 0;
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteBooks", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId",BookId);
                connection.Open();
                 result = cmd.ExecuteNonQuery();
                connection.Close();

                if(result!=0)
                {
                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public BookPostModel GetBookById(int BookId)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spGetBookById", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", BookId);

                BookPostModel model = new BookPostModel();

                connection.Open();
                SqlDataReader rdr=cmd.ExecuteReader();
                while(rdr.Read())
                {
            
                    model.BookId = Convert.ToInt32(rdr["BookId"]);
                    model.BookName = Convert.ToString(rdr["BookName"]);
                    model.AuthorName = Convert.ToString(rdr["AuthorName"]);
                    model.Description = Convert.ToString(rdr["Description"]);
                    model.ActualPrice = Convert.ToDouble(rdr["ActualPrice"]);
                    model.DiscountedPrice = Convert.ToDouble(rdr["DiscountedPrice"]);
                    model.Quantity = Convert.ToInt32(rdr["Quantity"]);
                    model.BookImage = Convert.ToString(rdr["BookImage"]);
                }
                connection.Close();
                if(model.BookId==BookId)
                {
                    return model;
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

        public BookPostModel UpdateBook(int BookID, BookPostModel bookPostModel)
        {
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId",BookID);
                cmd.Parameters.AddWithValue("@BookName", bookPostModel.BookName);
                cmd.Parameters.AddWithValue("@Description", bookPostModel.Description);
                cmd.Parameters.AddWithValue("@AuthorName", bookPostModel.AuthorName);
                cmd.Parameters.AddWithValue("@ActualPrice", bookPostModel.ActualPrice);
                cmd.Parameters.AddWithValue("@DiscountedPrice", bookPostModel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@BookImage", bookPostModel.BookImage);
                cmd.Parameters.AddWithValue("@Quantity", bookPostModel.Quantity);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if(result!=0)
                {
                    return bookPostModel;
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
