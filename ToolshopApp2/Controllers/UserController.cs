using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class UserController
    {
        public static Model.User GetUser()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();

            var users = GetUsers();
            foreach (var user in users)
            {
                if (user.Name == Environment.UserName)
                {
                    return user;
                }
            }
            return null;
        }
        public static bool IsUserToolshopMember()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            
            if (GetUser() != null)
            return _context.Users
                .Where(u => u.Name == Environment.UserName)
                .FirstOrDefault().KindOfUserId 
                == _context.KindOfUsers
                .Where(k => k.Id == 2)
                .FirstOrDefault().Id;
            return false;
        }
        public static bool IsUserAdministartor()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            if (GetUser() != null)
                return _context.Users
                .Where(u => u.Name == Environment.UserName)
                .FirstOrDefault().KindOfUserId
                == _context.KindOfUsers
                .Where(k => k.Id == 3)
                .FirstOrDefault().Id;
            return false;
        }
        public static IEnumerable<User> GetUsers()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            List<User> users = new List<User>();
            foreach (User user in _context.Users)
            {
                users.Add(user);
            }
            return users;
        }
    }
}
