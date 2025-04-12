namespace backend.DTO.UsersDTO
{
    public class UsersListDTO
    {
        public int TotalPageNumber { get; set; }
        public ICollection<UserInfoDTO> UsersList { get; set; }
    }
}
