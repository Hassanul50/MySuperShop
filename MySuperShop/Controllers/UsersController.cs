using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySuperShopModel.DTOs;
using MySuperShopModel.Entities;

namespace MySuperShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPasswordHasher<UserDTO> passwordHasher;
        public UsersController(IPasswordHasher<UserDTO> _passwordHasher)
        {
            passwordHasher = _passwordHasher;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("User Data is require ");
            }
           
                user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
                User userData = new User();
                userData.FirstName = user.FirstName;
                userData.PasswordHash = user.PasswordHash;
                userData.MobileNo = user.MobileNo;
                userData.AuthFlag = "U";
              
             
            return Ok(new
            {
                message = "user created successfully",
                data = userData.UserId

            });


        }
    }
}
