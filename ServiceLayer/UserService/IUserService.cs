using DataLayer.Entities;
using DataLayer.Models;

namespace ServiceLayer.UserService
{
    public interface IUserService
    {
        User AddRewardPoints(User user, int points);
        User UpdateUser(UserProfileModel model);
    }
}
