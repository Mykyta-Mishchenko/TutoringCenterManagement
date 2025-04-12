using backend.Data.DataModels;
using backend.DTO.UsersInfoDTO;

namespace backend.DTO.UsersDTO
{
    public class UserInfoDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public ICollection<SubjectInfoDTO> Subjects { get; set; }
    }
}
