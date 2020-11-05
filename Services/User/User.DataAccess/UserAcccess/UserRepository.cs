using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Data.Database;

namespace User.DataAccess.UserAcccess
{
    public class UserRepository : IUserRepository
    {
        private UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public bool CheckUserExists(string userName)
        {
            return _context.User.Any(e => e.Username == userName);
        }

        public User.Data.Database.ApplicationUser CreateNewUser(User.Data.Database.ApplicationUser newUser)
        {
            _context.User.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}
