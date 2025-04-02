using System.ComponentModel.DataAnnotations;

namespace backend.DTO.AuthDTO
{
    public class SignInDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
