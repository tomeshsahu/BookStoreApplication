using BusinessLayer.Interface;
using DatabaseLayer.AddressModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public string AddAddress(int UserId, AddressPostModel addressPostModel)
        {
            try
            {
                return this.addressRL.AddAddress(UserId, addressPostModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteAddress(int UserId, int AddressId)
        {
            try
            {
                return this.addressRL.DeleteAddress(UserId,AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddressPostModel GetAddress(int UserId, int AddressId)
        {
            try
            {
                return this.addressRL.GetAddress(UserId,AddressId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateAddress(int AddressId, AddressPostModel addressModel)
        {
            try
            {
                return this.addressRL.UpdateAddress(AddressId,addressModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
