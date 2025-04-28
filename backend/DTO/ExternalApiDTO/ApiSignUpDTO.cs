using backend.Models;
using System.Text.Json.Serialization;

namespace backend.DTO.ExternalApiDTO
{
    public class ApiSignUpDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
