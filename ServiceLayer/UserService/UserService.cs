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
    }
}
