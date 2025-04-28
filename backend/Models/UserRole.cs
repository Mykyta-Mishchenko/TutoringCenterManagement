using System.Runtime.Serialization;

namespace backend.Models
{
    public enum UserRole
    {
        [EnumMember(Value = "student")]
        student = 1,
        [EnumMember(Value = "teacher")]
        teacher = 2,
        [EnumMember(Value = "ApiClient")]
        apiClient = 3
    }
}
