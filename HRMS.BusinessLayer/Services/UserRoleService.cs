using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;
using HRMS.Entities.User.UserRoles.UserRolesRequestEntities;
using HRMS.Entities.User.UserRoles.UserRolesResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _rolesRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRoleReadResponseDto>> GetUserRoles()
        {
            var roles = await _rolesRepository.GetUserRoles();
            var response = _mapper.Map<IEnumerable<UserRoleReadResponseDto>>(roles);
            return response;
        }

        public async Task<UserRoleReadResponseDto?> GetUserRoleById(int? rolesId)
        {
            var role = await _rolesRepository.GetUserRoleById(rolesId);
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
            var addedRole = await _rolesRepository.CreateUserRole(rolesEntity);
            var response = _mapper.Map<UserRoleCreateResponseDto>(addedRole);
            return response;
        }

        public async Task<UserRoleUpdateResponseDto> UpdateUserRole(UserRoleUpdateRequestDto rolesDTo)
        {
            var rolesEntity = _mapper.Map<UserRoleUpdateRequestEntity>(rolesDTo);
            var updatedUserRoles = await _rolesRepository.UpdateUserRole(rolesEntity);
            var response = _mapper.Map<UserRoleUpdateResponseDto>(updatedUserRoles);
            return response;
        }

        public async Task<UserRoleDeleteResponseDto?> DeleteUserRole(UserRoleDeleteRequestDto rolesDto)
        {
            var rolesEntity = _mapper.Map<UserRoleDeleteRequestEntity>(rolesDto);
            var result = await _rolesRepository.DeleteUserRole(rolesEntity);

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
