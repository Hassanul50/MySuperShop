using MySuperShopModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySuperShopServies.Interface
{
    public interface ILogInService
    {
       Task<User> UserRegister(User user);
        Task<int> GetTotalUserCount();
        Task<User> getUserByEmail(string email);
       Task<User> getUserByUserId(string userId);
    }
}
