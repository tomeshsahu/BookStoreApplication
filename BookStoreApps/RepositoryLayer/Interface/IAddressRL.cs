using DatabaseLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public string AddAddress(int UserId,AddressPostModel addressPostModel);
        public string DeleteAddress(int UserId, int AddressId);

        public string UpdateAddress(int UserId,AddressPostModel addressPostModel);

        public AddressPostModel GetAddress(int UserId,int AddressId);
    }
}
