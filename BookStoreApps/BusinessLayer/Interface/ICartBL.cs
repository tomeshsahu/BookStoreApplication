using DatabaseLayer.CardModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public string AddToCard(int UserId,CartPostModel cartPostModel);

        public string DeleteCart(int UserId, int CartId);

        public string UpdateCart(int UserId, UpdatePostModel updatePostModel);

        public List<CartBook> GetAllBooksCart(int UserId);
    }
}
