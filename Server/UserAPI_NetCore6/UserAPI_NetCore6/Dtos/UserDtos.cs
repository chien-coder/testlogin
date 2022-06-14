using System.ComponentModel.DataAnnotations;

namespace UserAPI_NetCore6.Dtos
{
    public class LoginUserDtos
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class CreateUserDtos
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class UpdateUserDtos
    {
        [Required]
        public string password { get; set; }
    }

    public class LoginAminDtos
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
