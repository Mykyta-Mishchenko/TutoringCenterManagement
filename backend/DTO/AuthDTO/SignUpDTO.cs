using System.ComponentModel.DataAnnotations;

namespace backend.DTO.AuthDTO
{
    public class SignUpDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
