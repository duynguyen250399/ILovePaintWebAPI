using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Models;

namespace ServiceLayer.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.Where(u => u.UserName == username)
                .FirstOrDefault();
            if(user == null)
            {
                return null;
            }

            //if (!user.PasswordHashed.Equals(password))
            //{
            //    return null;
            //}

            return user;
        }

    }
}
