using BusinessLayer.Interface;
using DatabaseLayer.CardModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL=cartRL;
        }
        public string AddToCard(int UserId,CartPostModel cartPostModel)
        {
            try
            {
                return this.cartRL.AddToCart(UserId,cartPostModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteCart(int UserId, int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(UserId, CartId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CartBook> GetAllBooksCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllBooksCart(UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateCart(int UserId, UpdatePostModel updatePostModel)
        {
            try
            {
                return this.cartRL.UpdateCart(UserId, updatePostModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }

    

    }

