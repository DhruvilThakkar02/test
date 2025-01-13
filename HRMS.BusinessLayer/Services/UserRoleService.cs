using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.UserRole.UserRoleRequestDtos;
using HRMS.Dtos.User.UserRole.UserRoleResponseDtos;
using HRMS.Entities.User.UserRole.UserRoleRequestEntities;
using HRMS.Entities.User.UserRole.UserRoleResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRoleReadResponseDto>> GetUserRoles()
        {
            var roles = await _roleRepository.GetUserRoles();
            var response = _mapper.Map<IEnumerable<UserRoleReadResponseDto>>(roles);
            return response;
        }

        public async Task<UserRoleReadResponseDto?> GetUserRoleById(int? rolesId)
        {
            var role = await _roleRepository.GetUserRoleById(rolesId);
            if (role == null || role.UserRoleId == -1)
            {
                return null;
            }
            var response = _mapper.Map<UserRoleReadResponseDto>(role);
            return response;
        }

        public async Task<UserRoleCreateResponseDto> CreateUserRole(UserRoleCreateRequestDto rolesDto)
        {
            var rolesEntity = _mapper.Map<UserRoleCreateRequestEntity>(rolesDto);
            var addedRole = await _roleRepository.CreateUserRole(rolesEntity);
            var response = _mapper.Map<UserRoleCreateResponseDto>(addedRole);
            return response;
        }

        public async Task<UserRoleUpdateResponseDto> UpdateUserRole(UserRoleUpdateRequestDto rolesDTo)
        {
            var rolesEntity = _mapper.Map<UserRoleUpdateRequestEntity>(rolesDTo);
            var updatedUserRole = await _roleRepository.UpdateUserRole(rolesEntity);
            var response = _mapper.Map<UserRoleUpdateResponseDto>(updatedUserRole);
            return response;
        }

        public async Task<UserRoleDeleteResponseDto?> DeleteUserRole(UserRoleDeleteRequestDto rolesDto)
        {
            var rolesEntity = _mapper.Map<UserRoleDeleteRequestEntity>(rolesDto);
            var result = await _roleRepository.DeleteUserRole(rolesEntity);

            if (result == -1)
            {
                return null;
            }

            var responseEntity = new UserRoleDeleteResponseEntity { UserRoleId = rolesEntity.UserRoleId };
            var responseDto = _mapper.Map<UserRoleDeleteResponseDto>(responseEntity);

            return responseDto;
        }
    }
}
