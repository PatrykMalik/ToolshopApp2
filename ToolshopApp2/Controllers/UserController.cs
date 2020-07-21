using System;
using System.Collections.Generic;
using System.Linq;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class UserController
    {
        static DatabaseConnectionContext _context = new DatabaseConnectionContext();

        public static bool UserExistInDatabase()
        {
            return _context.Users.Where(x => x.Name == Environment.UserName.ToLower()).FirstOrDefault() != null;
        }

        public static bool IsUserToolshopMemberOrAdministator()
        {
            return _context.Users.Where(u => u.Name == Environment.UserName.ToLower() && u.KindOfUserId > 1).FirstOrDefault() != null;
        }

        public static bool IsUserAdministartor()
        {
            return _context.Users.Where(u => u.Name == Environment.UserName.ToLower() && u.KindOfUserId == 3).FirstOrDefault() != null;
        }

        public static User GetUser(string name)
        {
            return _context.Users.Where(x => x.Name == name).FirstOrDefault();
        }

        public static IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList<User>();
        }

        public static bool AddUser(string email)
        {
            if (!UserExistInDatabase())
            {
                var user = new User
                {
                    Name = Environment.UserName.ToLower(),
                    Emial = email,
                    KindOfUserId = 1
                };
                _context.Add(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
