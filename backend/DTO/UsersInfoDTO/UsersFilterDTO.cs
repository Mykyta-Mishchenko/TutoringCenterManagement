using backend.Models;
using System.Text.Json.Serialization;

namespace backend.DTO.UsersDTO
{
    public class UsersFilterDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        public string? Name { get; set; }
        public int SubjectId { get; set; }
        public int SchoolYear { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}
