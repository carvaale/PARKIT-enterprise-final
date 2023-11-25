namespace PARKIT_enterprise_final.Models.Interfaces
{
    public interface IUserProvider
    {
        User GetUser(Guid userId);
        User AddUser(User user);
        User UpdateUser(User user);
    }
}
