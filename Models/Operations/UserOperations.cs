using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PARKIT_enterprise_final.Models.DBContext;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Models.Operations
{
    public class UserOperations : IUserProvider
    {
        private readonly PARKITDBContext _parkitDb;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserOperations(PARKITDBContext parkitDb, IHttpContextAccessor contextAccessor)
        {
            _parkitDb = parkitDb;
            _contextAccessor = contextAccessor;
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

        public User GetSessionUser()
        {
            if(_contextAccessor.HttpContext != null)
            {
                ISession session = _contextAccessor.HttpContext.Session;
                string? value = session.GetString("CurrentUser");
                if(value != null)
                {
                    User? user = JsonConvert.DeserializeObject<User?>(value);
                    return user;
                }
                return null;
            }
            return null;
        }

        public User GetUser(Guid userId)
        {
            return _parkitDb.Users.Find(userId);
        }

        public User GetUserByUsername(string username)
        {
            User user = _parkitDb.Users.FirstOrDefault(x => x.Username == username);
            return user;
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
