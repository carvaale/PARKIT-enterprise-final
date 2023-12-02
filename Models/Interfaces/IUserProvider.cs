namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IUserProvider
    {
        List<User> GetUsers();
        User GetUser(Guid userId);
        User CreateUser(User user);
        User UpdateUser(User user);
        User GetUserByUsername(string username);
        void DeleteUser(User user);
        User GetSessionUser();
    }
}
