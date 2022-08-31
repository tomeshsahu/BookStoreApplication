using DatabaseLayer.UserPostModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegisterModel Registration(UserRegisterModel userRegistrationModel);

        public string Login(UserLogin userLogin);
        public bool ForgetPasswordUser(string email);

        public bool ResetPassword(string email,PasswordModel passwordModel);

    }
}
