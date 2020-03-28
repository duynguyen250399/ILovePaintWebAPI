using DataLayer.Entities;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.UserService
{
    public interface IUserService
    {
        User AddRewardPoints(User user, int points);
      
    }
}
