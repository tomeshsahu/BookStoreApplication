using DatabaseLayer.CardModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public string AddToCart(int UserId,CartPostModel cartPostModel);

        public string DeleteCart(int UserId, int CartId);
        public string UpdateCart(int UserId, UpdatePostModel updatePostModel);

         public List<CartBook> GetAllBooksCart(int UserId);
    }
}
