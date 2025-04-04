using System.Runtime.Serialization;

namespace backend.Models
{
    public enum UserRole
    {
        [EnumMember(Value = "student")]
        student,
        [EnumMember(Value = "teacher")]
        teacher
    }
}
