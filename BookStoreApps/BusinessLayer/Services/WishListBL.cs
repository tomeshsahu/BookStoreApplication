using BusinessLayer.Interface;
using DatabaseLayer.WishListModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishlistBL
    {
        public IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }
        public string AddToWishList(int UserId, WishlistPostModel wishlistPostModel)
        {
            try
            {
                return this.wishListRL.AddToWishList(UserId, wishlistPostModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteWishList(int UserId, int WishListId)
        {
            try
            {
                return this.wishListRL.DeleteWishList(UserId, WishListId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListResponseModel> GetWishlistByUserid(int UserId)
        {
            try
            {
                return this.wishListRL.GetWishlistByUserid(UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
