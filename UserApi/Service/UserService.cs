using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Service
{
    public class UserService
    {
        private readonly UserApiDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        // Constructor
        public UserService(UserApiDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // Example method GetAll
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        // Example method GetById
        public User GetById(int id)
        {
            return _context.Users.Find(id) ?? throw new Exception("User not found");
        }

        // Example method Create
        public async Task<User> Create(User user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Example method Update
        public User Update(int id, User user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;
            existingUser.BirthDate = user.BirthDate;

            _context.Users.Update(existingUser);
            _context.SaveChanges();
            return existingUser;
        }

        // Example method Delete
        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // Example method GetByUsernameOrEmail
        public User GetByUsernameOrEmail(string username, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username || u.Email == email);
            return user ?? throw new Exception("User not found");
        }
    }
}