
namespace User.DataAccess.UserAcccess
{
    public interface IUserRepository
    {
        bool CheckUserExists(string userName);
        Data.Database.ApplicationUser CreateNewUser(Data.Database.ApplicationUser newUser);
    }
}