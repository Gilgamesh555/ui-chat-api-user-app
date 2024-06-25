using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Dtos;
using UserApi.Models;

namespace UserApi.Service
{
    public class AuthService
    {
        private readonly UserApiDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(UserApiDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Authenticate_User(LoginRequest user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            var result = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, user.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid password");
            }

            return existingUser;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }
    }
}