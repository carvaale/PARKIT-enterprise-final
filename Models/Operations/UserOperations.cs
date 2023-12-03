/// <summary>
/// Created by Ruiyan Shi 
/// standard CRUD operations for accessing the User database table
/// provide function to access the table for the whole project
/// </summary>
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

        // create the user in the users table
        public User CreateUser(User user)
        {
            _parkitDb.Users.Add(user);
            _parkitDb.SaveChanges();
            return user;
        }

        // delete the user from table
        public void DeleteUser(User user)
        {
            _parkitDb.Users.Remove(user);
            _parkitDb.SaveChanges();
        }

        // whenever a user login, using the session to remember who loged in
        // provide a function to get a current user in the session
        // return the user object
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

        // get user by is
        public User GetUser(Guid userId)
        {
            return _parkitDb.Users.Find(userId);
        }

        // get the user with the provided user name
        public User GetUserByUsername(string username)
        {
            if(_parkitDb != null)
            {
                User? user = _parkitDb.Users?.FirstOrDefault(x => x.Username == username);
                return user;
            }
            return null;
            
        }

        // get all users
        public List<User> GetUsers()
        {
            List<User> Users = _parkitDb.Users.ToList();
            return Users;
        }

        // update the user
        public User UpdateUser(User user)
        {
            var u = _parkitDb.Users.Attach(user);
            u.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _parkitDb.SaveChanges();
            return user;
        }

    }
}
