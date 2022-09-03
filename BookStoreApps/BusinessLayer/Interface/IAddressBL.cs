using DatabaseLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(int UserId, AddressPostModel addressPostModel);

        public string DeleteAddress(int UserId, int AddressId);

        public string UpdateAddress(int AddressId, AddressPostModel addressModel);

        public AddressPostModel GetAddress(int UserId, int AddressId);
    }
}
