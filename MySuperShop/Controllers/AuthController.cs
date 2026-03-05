using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MySuperShopModel.DTOs.Request;
using MySuperShopModel.Entities;
using MySuperShopServies.Interface;

namespace MySuperShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly ILogInService _logInService;
        public AuthController(ILogInService logInService)
        {
            _logInService = logInService;
        }
        [HttpPost("LogIn")]
        public IActionResult LogIn([FromBody] LogInRequestDto request)
        {
            User user = FindUser(request.UserIdOrEmail);
            if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                var Result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (Result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid password" });

            }
            return Ok(new
            {
                message = "Login successfully",
                data = "token"
            });



        }
        private User FindUser(string userinfo)
        {
            if (string.IsNullOrWhiteSpace(userinfo))
                throw new ArgumentException("User information is required", nameof(userinfo));
            if (userinfo.Contains("@"))
            {
                return _logInService.getUserByEmail(userinfo).Result;
            }

            else
            {
                return _logInService.getUserByUserId(userinfo).Result;
            }

        }
    }
}
