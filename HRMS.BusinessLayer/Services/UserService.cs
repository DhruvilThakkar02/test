using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Entities.User.User.UserRequestEntities;
using HRMS.Entities.User.User.UserResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.Passwords;

namespace HRMS.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadResponseDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            foreach (var user in users)
            {
                user.Password = PasswordHashingUtility.HashPassword(user.Password);
            }
            var response = _mapper.Map<IEnumerable<UserReadResponseDto>>(users);
            return response;
        }

        public async Task<UserReadResponseDto?> GetUserById(int? userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null || user.UserId == -1)
            {
                return null;
            }

            user.Password = PasswordHashingUtility.HashPassword(user.Password);

            var response = _mapper.Map<UserReadResponseDto>(user);
            return response;
        }

        public async Task<UserCreateResponseDto> CreateUser(UserCreateRequestDto userDto)
        {
            var userEntity = _mapper.Map<UserCreateRequestEntity>(userDto);
            var addedUser = await _userRepository.CreateUser(userEntity);
            var response = _mapper.Map<UserCreateResponseDto>(addedUser);
            return response;
        }

        public async Task<UserUpdateResponseDto> UpdateUser(UserUpdateRequestDto userDto)
        {
            var userEntity = _mapper.Map<UserUpdateRequestEntity>(userDto);
            var updatedUser = await _userRepository.UpdateUser(userEntity);
            var response = _mapper.Map<UserUpdateResponseDto>(updatedUser);
            return response;
        }

        public async Task<UserDeleteResponseDto?> DeleteUser(UserDeleteRequestDto userDto)
        {
            var userEntity = _mapper.Map<UserDeleteRequestEntity>(userDto);
            var result = await _userRepository.DeleteUser(userEntity);
            if (result == -1)
            {
                return null;
            }
            var responseEntity = new UserDeleteResponseEntity { UserId = userEntity.UserId };
            var responseDto = _mapper.Map<UserDeleteResponseDto>(responseEntity);

            return responseDto;
        }
    }
}