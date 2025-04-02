using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.DataModels
{
    public class User
    {
        [BindNever]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
        public ICollection<RefreshSession> Sessions { get; set; }
    }
}
