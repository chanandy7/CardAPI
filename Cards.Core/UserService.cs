//using Cards.Core.customExceptions;
//using Cards.Core.CustomExceptions;
//using Cards.Core.DTO;
//using Cards.Core.Utilities;
//using Cards.DB;
//using Microsoft.AspNet.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Cards.Core
//{
//    public class UserService : IUserService
//    {
//        //for dependency injection
//        private readonly AppDbContext _context;
//        private readonly IPasswordHasher _passwordHasher;


//        public UserService(AppDbContext context, IPasswordHasher passwordHasher)
//        {
//            _context = context;
//            _passwordHasher = passwordHasher;
//        }

//        public async Task<AuthenticatedUser> SignIn(User user)
//        {
//            //get all the users
//            var dbUser = await _context.Users
//                .FirstOrDefaultAsync(u => u.Username == user.Username);

//            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
//            {
//                throw new InvalidUsernamePasswordException("Invalid username or password");
//            }

//            return new AuthenticatedUser
//            {
//                Username = user.Username,
//                Token = JwtGenerator.GenerateUserToken(user.Username)
//            };
//        }

//        public async Task<AuthenticatedUser> SignUp(User user)
//        {
//            var checkUser = await _context.Users
//                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));
//            if (checkUser != null)
//            {
//                throw new UsernameAlreadyExistsException("Username already exists");
//            }
//            user.Password = _passwordHasher.HashPassword(user.Password);
//            await _context.AddAsync(user);
//            await _context.SaveChangesAsync();

//            return new AuthenticatedUser
//            {
//                Username = user.Username,
//                Token = JwtGenerator.GenerateUserToken(user.Username)
//            };

            
//        }
//    }
//}







using Cards.Core.customExceptions;
using Cards.Core.CustomExceptions;
using Cards.Core.DTO;
using Cards.Core.Utilities;
using Cards.DB;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Core
{
    public class UserService : IUserService
    {
        //for dependency injection
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;


        public UserService(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            //get all the users
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));
            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            user.Password = _passwordHasher.HashPassword(user.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };


        }
    }
}