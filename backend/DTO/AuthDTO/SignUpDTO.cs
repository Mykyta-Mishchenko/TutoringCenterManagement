using backend.Data.DataModels;
using backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.DTO.AuthDTO
{
    public class SignUpDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
