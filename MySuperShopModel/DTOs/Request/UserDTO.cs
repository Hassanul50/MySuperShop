using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MySuperShopModel.DTOs.Request
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }
        [Required]
        [Phone]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile number must be 11 digits")]
        [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Invalid Bangladeshi mobile number")]
        public string MobileNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
       
    }
}
