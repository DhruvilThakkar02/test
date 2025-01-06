using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class UserRolesMappingService : IUserRoleMappingService
    {
        private readonly IMapper _mapper;
        private readonly IUserRoleMappingRepository _userRolesMappingRepository;

        public UserRolesMappingService(IMapper mapper, IUserRoleMappingRepository userRolesMappingRepository)
        {

            _mapper = mapper;
            _userRolesMappingRepository = userRolesMappingRepository;
        }

        public async Task<IEnumerable<UserRoleMappingReadResponseDto>> GetUserRolesMapping()
        {
            var mappingroles = await _userRolesMappingRepository.GetUserRolesMapping();
            var response = _mapper.Map<IEnumerable<UserRoleMappingReadResponseDto>>(mappingroles);
            return response;
        }

        public async Task<UserRoleMappingReadResponseDto?> GetUserRoleMappingById(int? roleid)
        {
            var role = await _userRolesMappingRepository.GetUserRoleMappingById(roleid);
            if (role == null || role.UserRoleMappingId == -1)
            {
                return null;
            }
            var response = _mapper.Map<UserRoleMappingReadResponseDto>(role);
            return response;
        }

        public async Task<UserRoleMappingCreateResponseDto> CreateUserRoleMapping(UserRoleMappingCreateRequestDto rolesMappingDto)
        {
            var rolesMappingEntity = _mapper.Map<UserRoleMappingCreateRequestEntity>(rolesMappingDto);
            var addedUserMappingRole = await _userRolesMappingRepository.CreateUserRoleMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRoleMappingCreateResponseDto>(addedUserMappingRole);
            return response;
        }
        public async Task<UserRoleMappingUpdateResponseDto?> UpdateUserRolesMapping(UserRoleMappingUpdateRequestDto rolesMappingDto)
        {
            var rolesMappingEntity = _mapper.Map<UserRoleMappingUpdateRequestEntity>(rolesMappingDto);
            var UpdatedMappingRole = await _userRolesMappingRepository.UpdateUserRoleMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRoleMappingUpdateResponseDto>(UpdatedMappingRole);
            return response;
        }

        public async Task<UserRoleMappingDeleteResponseDto?> DeleteUserRoleMapping(UserRoleMappingDeleteRequestDto rolesMappingDto)
        {
            var rolesEntity = _mapper.Map<UserRoleMappingDeleteRequestEntity>(rolesMappingDto);
            var result = await _userRolesMappingRepository.DeleteUserRoleMapping(rolesEntity);

            if (result == -1)
            {
                return null;
            }

            var responseEntity = new UserRoleMappingDeleteResponseEntity { UserRoleMappingId = rolesEntity.UserRoleMappingId };
            var responseDto = _mapper.Map<UserRoleMappingDeleteResponseDto>(responseEntity);

            return responseDto;
        }

    }
}
