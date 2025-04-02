using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace backend.Data.DataModels
{
    public class Role
    {
        [BindNever]
        public int RoleId { get; set; }
        public string Name { get; set; }

        public ICollection<UserRoles> Roles { get; set; }
    }
}
