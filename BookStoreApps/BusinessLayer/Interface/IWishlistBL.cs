using DatabaseLayer.WishListModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishlistBL
    {
        public string AddToWishList(int UserId, WishlistPostModel wishlistPostModel);

        public string DeleteWishList(int UserId, int WishListId);

        public List<WishListResponseModel> GetWishlistByUserid(int UserId);
    }
}
