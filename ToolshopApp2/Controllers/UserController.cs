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

            var user = _context.Users
                .Where(u => u.Name == Environment.UserName)
                .FirstOrDefault();
            return user;
        }
        public static bool IsUserToolshopMember()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();

            return _context.Users
                .Where(u => u.Name == Environment.UserName)
                .FirstOrDefault().KindOfUserId 
                == _context.KindOfUsers
                .Where(k => k.Id == 2)
                .FirstOrDefault().Id;
        }
        public static bool IsUserAdministartor()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();

            return _context.Users
                .Where(u => u.Name == Environment.UserName)
                .FirstOrDefault().KindOfUserId
                == _context.KindOfUsers
                .Where(k => k.Id == 3)
                .FirstOrDefault().Id;
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
