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

        public User AddRewardPoints(User user, int points)
        {
            user.RewardPoints = user.RewardPoints + points;
            _context.Update(user);

            _context.SaveChanges();

            return user;
        }

        public User UpdateUser(UserProfileModel model)
        {
            var user = _context.Users.Where(u => u.Id == model.UserID)
                .FirstOrDefault();
            if(user == null)
            {
                return null;
            }

            user.FullName = model.FullName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
           
            user.Gender = model.Gender;

            // process user avatar upload
            string path = @"/uploads/images/users/" + model.Avatar.FileName;
            user.Image = path;

            _context.Users.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
