using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Domain
{
    public interface IUserRepo
    {
        List<User> GetAllMembers();

        ///User CreateUser(User user);

        User GetUserById(string id);

        User GetUserLogin(User user);

        string CheckUniqueEmail(string email);

        string CheckUniqueUsername(string username);
    }
}
