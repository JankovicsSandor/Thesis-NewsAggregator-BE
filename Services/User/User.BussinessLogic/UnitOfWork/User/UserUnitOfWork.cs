using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using User.BussinessLogic.UnitOfWork.Password;
using User.Data.Database;
using User.DataAccess.UserAcccess;
using User.Shared.Models.Input;
using User.Shared.Models.Output;
using User.Shared.Validation;

namespace User.BussinessLogic.UnitOfWork.User
{
    public class UserUnitOfWork
    {
        private IPasswordUnitOfWork _passwordLogic;
        private IUserRepository _userRepo;
        private IMapper _mapper;

        public UserUnitOfWork(IPasswordUnitOfWork passwordLogic, IUserRepository userRepo, IMapper mapper)
        {
            _passwordLogic = passwordLogic;
            _userRepo = userRepo;
            _mapper = mapper;

        }

        public bool CheckUserNameExists(string userName)
        {
            return _userRepo.CheckUserExists(userName);
        }

        public UserCreateResponse CreateNewUser(CreateNewUserInputModel newUser)
        {
            if (_userRepo.CheckUserExists(newUser.UserName))
            {
                throw new CreateUserException("User name is already taken");
            }

            byte[] passwordHash, passwordSalt;
            _passwordLogic.CreatePasswordHash(newUser.Password, out passwordHash, out passwordSalt);

            ApplicationUser actualNewUser = _mapper.Map<ApplicationUser>(newUser);

            actualNewUser.RegisterDate = DateTime.Now;
            actualNewUser.Uuid = Guid.NewGuid().ToString();
            actualNewUser.Password = passwordHash;
            actualNewUser.PasswordSalt = passwordSalt;

            _userRepo.CreateNewUser(actualNewUser);
            //TODO return user token;

            return null;
        }
    }
}
