using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Agency.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmai { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
