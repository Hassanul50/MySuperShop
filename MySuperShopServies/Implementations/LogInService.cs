using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySuperShopData.DB;
using MySuperShopModel.Entities;
using MySuperShopServies.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySuperShopServies.Implementations
{
    public class LogInService:ILogInService
    {
        public readonly SuperShopDBContex _superShopDBContex;
        public LogInService(SuperShopDBContex superShopDBContex)
        {
           _superShopDBContex = superShopDBContex;
        }

        public async Task<User> getUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("email is required", nameof(email));
            return  await _superShopDBContex.users.FirstOrDefaultAsync(u => u.Email == email);
          

        }
        public async Task<int> GetTotalUserCount()
        {
            return await _superShopDBContex.users.CountAsync();
        }
        public Task<User> getUserByUserId(string userId)
        {
           if(string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("userId is required", nameof(userId));
            return _superShopDBContex.users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);
        }

        public async Task<User> UserRegister(User user)
        {
                _superShopDBContex.users.Add(user);
                await _superShopDBContex.SaveChangesAsync();
                return user;
        }

    }
}
