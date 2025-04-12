using backend.DTO.UsersDTO;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;

namespace backend.Services
{
    public class UsersService : IUsersService
    {
        private IUserRepository _usersRepository;
        public UsersService(IUserRepository userRepository)
        {
            _usersRepository = userRepository;
        }
        public async Task<UsersListDTO> GetUsersByFilterAsync(UsersFilterDTO filter)
        {
            return await _usersRepository.GetUsersByFilterAsync(filter);
        }
    }
}
