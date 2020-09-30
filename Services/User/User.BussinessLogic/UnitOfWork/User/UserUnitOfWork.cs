using System;
using System.Collections.Generic;
using System.Text;
using User.BussinessLogic.UnitOfWork.Password;
using User.Shared.Models.Input;
using User.Shared.Models.Output;

namespace User.BussinessLogic.UnitOfWork.User
{
    public class UserUnitOfWork
    {
        private IPasswordUnitOfWork _passwordLogic;

        public UserUnitOfWork(IPasswordUnitOfWork passwordLogic)
        {
            _passwordLogic = passwordLogic;
        }

        public UserCreateResponse CreateNewUser(CreateNewUserInputModel newUser)
        {
            return null;
        }
    }
}
