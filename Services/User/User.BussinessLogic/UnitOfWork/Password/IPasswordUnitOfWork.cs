namespace User.BussinessLogic.UnitOfWork.Password
{
    public interface IPasswordUnitOfWork
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}