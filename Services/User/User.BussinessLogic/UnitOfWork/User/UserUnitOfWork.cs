using System;
using System.Collections.Generic;
using System.Text;
using User.BussinessLogic.UnitOfWork.Password;

namespace User.BussinessLogic.UnitOfWork.User
{
    public class UserUnitOfWork
    {
        private IPasswordUnitOfWork _passwordLogic;

        public UserUnitOfWork(IPasswordUnitOfWork passwordLogic)
        {
            _passwordLogic = passwordLogic;
        }

        public void CreateNewUser()
    }
}
