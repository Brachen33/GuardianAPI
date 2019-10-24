using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context; 
        }


        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;            
        }
       
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUser(int Id)
        {
            return _context.Users.Find(Id);
        }

        public User Update(User userChanges)
        {
            var user = _context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return userChanges;
        }
    }
}

