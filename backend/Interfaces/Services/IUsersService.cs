using backend.DTO.UsersDTO;

namespace backend.Interfaces.Services
{
    public interface IUsersService
    {
        public Task<UsersListDTO> GetUsersByFilterAsync(UsersFilterDTO filter);
    }
}
