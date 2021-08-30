using P1RestaurantReviewer.DataAccess.Entities;
using P1RestaurantReviewer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace P1RestaurantReviewer.DataAccess
{
    /// <summary>
    /// Handles all User database requests
    /// </summary>
    public class UserRepo : IUserRepo
    {

        /// <summary>
        /// referencing the Entities context
        /// </summary>
        private restaurantreviewerContext _context;

        /// <summary>
        /// injecting the context into the UsersRepo class
        /// </summary>
        /// <param name="context"></param>
        public UserRepo(restaurantreviewerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all members in the database and returns a list
        /// </summary>
        /// <returns>List of members</returns>
        public List<Domain.User> GetAllMembers()
        {
            //Console.WriteLine("You're in UsersRepo");
            return _context.Users.Select(
                u => new Domain.User(u.Id, u.FirstName, u.LastName, u.Username, u.Email, u.IsAdmin)
            ).ToList();
        }
        /// <summary>
        /// Inserts Member data
        /// </summary>
        /// <param name="member"></param>
        /// <returns>user input and SQL created ID</returns>
        public Domain.User CreateUser(Domain.User user)
        {
            var newEntity = new Entities.User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
            _context.Users.Add(newEntity);
            _context.SaveChanges();
            user.Id = newEntity.Id;
            return user;
        }
        /// <summary>
        /// Get's member object by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Member data</returns>
        public Domain.User GetUserById(int id)
        {
            Entities.User foundUser = _context.Users.FirstOrDefault(
                u => u.Id == id
            );

            if (foundUser != null)
            {
                return new Domain.User(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Username, foundUser.Email, foundUser.IsAdmin);
            }
            return new Domain.User();
        }
        /// <summary>
        /// Checks if username and password are matching a user and returns member
        /// </summary>
        /// <param name="member"></param>
        /// <returns>Models.Member user</returns>
        public Domain.User GetUserLogin(Domain.User user)
        {
            Entities.User foundUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (foundUser != null)
            {
                return new Domain.User(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Username, foundUser.Email, foundUser.IsAdmin);
            }
            return new Domain.User();
        }

        /// <summary>
        /// Finds if email is in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>string found email</returns>
        public string CheckUniqueEmail(string email)
        {
            var foundUser = _context.Users.FirstOrDefault(
                user => user.Email == email
            );

            if (foundUser != null)
            {
                return email = foundUser.Email;
            }
            return email = "";

        }
        /// <summary>
        /// Finds if username is in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>string found username</returns>
        public string CheckUniqueUsername(string username)
        {
            Entities.User foundUser = _context.Users.FirstOrDefault(
               u => u.Username == username
           );

            if (foundUser != null)
            {
                return username = foundUser.Username;
            }
            return username = "";
        }
    }
}
