using System;
using System.Collections.Generic;
using System.Linq;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class UserController
    {
        //static DatabaseConnectionContext _context = new DatabaseConnectionContext();

        public static bool UserExistInDatabase()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            return _context.Users.Where(x => x.Name == Environment.UserName.ToLower()).FirstOrDefault() != null;
        }

        public static bool IsUserToolshopMemberOrAdministator()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            return _context.Users.Where(u => u.Name == Environment.UserName.ToLower() && u.KindOfUserId > 1).FirstOrDefault() != null;
        }

        public static bool IsUserAdministartor()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            return _context.Users.Where(u => u.Name == Environment.UserName.ToLower() && u.KindOfUserId == 3).FirstOrDefault() != null;
        }

        public static bool IsUserAdministartor(User user)
        {
            if (user == null)
            {
                return false;
            }
            return user.KindOfUserId == 3;
        }

        public static User GetUser(string name)
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            return _context.Users.Where(x => x.Name == name).FirstOrDefault();
        }

        public static User GetUser()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            return _context.Users.Where(x => x.Name == Environment.UserName.ToLower()).FirstOrDefault();
        }

        public static IEnumerable<KindOfUser> GetKindOfUsers()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            return _context.KindOfUsers.ToList<KindOfUser>();
        }
        public static IEnumerable<User> GetUsers()
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
            return _context.Users.ToList<User>();
        }

        public static bool AddUser(string email)
        {
            DatabaseConnectionContext _context = new DatabaseConnectionContext();
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

        public static bool UpdateUser(string email)
        {
            var _context = new DatabaseConnectionContext();
            if (UserExistInDatabase())
            {
                var user = GetUser();
                user.Emial = email;
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
