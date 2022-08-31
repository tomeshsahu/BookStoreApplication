using BusinessLayer.Interface;
using DatabaseLayer.UserPostModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        public IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }


        public UserRegisterModel Registration(UserRegisterModel userRegistrationModel)
        {
            try
            {
                return this.userRL.Registration(userRegistrationModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string Login(UserLogin userLogin)
        {
            try
            {
                return this.userRL.Login(userLogin);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPasswordUser(string email)
        {
            try
            {
                return this.userRL.ForgetPasswordUser(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassoword(string email, PasswordModel passwordModel)
        {
            try
            {
                return this.userRL.ResetPassword(email, passwordModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    
    }
}
