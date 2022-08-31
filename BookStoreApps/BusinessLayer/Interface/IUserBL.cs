using DatabaseLayer.UserPostModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegisterModel Registration(UserRegisterModel userRegistrationModel);
        public string Login(UserLogin userLogin);

        public bool ForgetPasswordUser(string email);

        public bool ResetPassoword(string email, PasswordModel modelPassword);
    }
}
