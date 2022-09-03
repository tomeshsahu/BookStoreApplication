using BusinessLayer.Interface;
using DatabaseLayer.BookModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        public IBookRL bookRL;

        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
         public BookPostModel AddBook(BookPostModel bookPostModel)
        {
            try
            {
                return this.bookRL.AddBook(bookPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<BookPostModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteBook(int BookId)
        {
            try
            {
                return this.bookRL.DeleteBook(BookId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public BookPostModel GetBookById(int BookId)
        {
            try
            {
                return this.bookRL.GetBookById(BookId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public BookPostModel UpdateBook(int BookID, BookPostModel bookPostModel)
        {
            try
            {
                return this.bookRL.UpdateBook(BookID, bookPostModel);   
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
