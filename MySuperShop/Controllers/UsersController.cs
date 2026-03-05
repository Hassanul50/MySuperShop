using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySuperShopModel.DTOs.Request;
using MySuperShopModel.Entities;
using MySuperShopServies.Interface;

namespace MySuperShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPasswordHasher<UserDTO> passwordHasher;
        private readonly ILogInService _logInService;
        public UsersController(IPasswordHasher<UserDTO> _passwordHasher, ILogInService logInService)
        {
            passwordHasher = _passwordHasher;
            _logInService = logInService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            if (user.FirstName == null)
            {
                return BadRequest("User Data is require ");
            }
            int totalUsers = await _logInService.GetTotalUserCount();
            string customUserId = $"{user.FirstName}_{totalUsers + 1}";
            //user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
            User userData = new User
            {   UserId = Guid.NewGuid().ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = passwordHasher.HashPassword(user, user.Password),
                MobileNo = user.MobileNo,
                CreatedDate = user.CreatedDate,
                AuthFlag = "U"
            };
            var saveUser = await _logInService.UserRegister(userData);

            return Ok(new
            {
                message = "user created successfully",
                data = saveUser.UserId

            });


        }
    }
}
