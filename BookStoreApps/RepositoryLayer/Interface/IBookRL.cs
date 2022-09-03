using DatabaseLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookPostModel AddBook(BookPostModel bookPostModel);

        public List<BookPostModel> GetAllBooks();

        public int DeleteBook(int BookId);
        public BookPostModel GetBookById(int BookId);

        public BookPostModel UpdateBook(int BookID,BookPostModel bookPostModel);

    }
}
