using System;
using System.Collections.Generic;
using System.Text;

namespace MySuperShopModel.DTOs.Request
{
    public class LogInRequestDto
    {
        public string UserIdOrEmail { get; set; }
        public string Password { get; set; }
    }
}
