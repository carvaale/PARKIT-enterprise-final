using Microsoft.EntityFrameworkCore;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;
using System.Net;
using System.Reflection;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class UserOperations : IUserProvider
    {
        private readonly PARKITDBContext _parkitDb;

        public UserOperations(PARKITDBContext parkitDb)
        {
            _parkitDb = parkitDb;
        }
        public User CreateUser(User user)
        {
            _parkitDb.Users.Add(user);
            _parkitDb.SaveChanges();
            return user;
        }

        public void DeleteUser(User user)
        {
            _parkitDb.Users.Remove(user);
            _parkitDb.SaveChanges();
        }

        public User GetUser(Guid userId)
        {
            return _parkitDb.Users.Find(userId);
        }

        public List<User> GetUsers()
        {
            List<User> Users = _parkitDb.Users.ToList();
            return Users;
        }

        public User UpdateUser(User user)
        {
            var u = _parkitDb.Users.Attach(user);
            u.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _parkitDb.SaveChanges();
            return user;
        }

    }
}
