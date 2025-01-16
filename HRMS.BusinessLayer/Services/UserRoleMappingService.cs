using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingRequestDtos;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingResponseDtos;
using HRMS.Entities.User.UserRoleMapping.UserRoleMappingRequestEntities;
using HRMS.Entities.User.UserRoleMapping.UserRoleMappingResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class UserRoleMappingService : IUserRoleMappingService
    {
        private readonly IMapper _mapper;
        private readonly IUserRoleMappingRepository _userRoleMappingRepository;

        public UserRoleMappingService(IMapper mapper, IUserRoleMappingRepository userRoleMappingRepository)
        {

            _mapper = mapper;
            _userRoleMappingRepository = userRoleMappingRepository;
        }

        public async Task<IEnumerable<UserRoleMappingReadResponseDto>> GetUserRolesMapping()
        {
            var mappingroles = await _userRoleMappingRepository.GetUserRolesMapping();
            var response = _mapper.Map<IEnumerable<UserRoleMappingReadResponseDto>>(mappingroles);
            return response;
        }

        public async Task<UserRoleMappingReadResponseDto?> GetUserRoleMappingById(int? roleid)
        {
            var role = await _userRoleMappingRepository.GetUserRoleMappingById(roleid);
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
            var addedUserMappingRole = await _userRoleMappingRepository.CreateUserRoleMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRoleMappingCreateResponseDto>(addedUserMappingRole);
            return response;
        }
        public async Task<UserRoleMappingUpdateResponseDto?> UpdateUserRoleMapping(UserRoleMappingUpdateRequestDto rolesMappingDto)
        {
            var rolesMappingEntity = _mapper.Map<UserRoleMappingUpdateRequestEntity>(rolesMappingDto);
            var UpdatedMappingRole = await _userRoleMappingRepository.UpdateUserRoleMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRoleMappingUpdateResponseDto>(UpdatedMappingRole);
            return response;
        }

        public async Task<UserRoleMappingDeleteResponseDto?> DeleteUserRoleMapping(UserRoleMappingDeleteRequestDto rolesMappingDto)
        {
            var rolesEntity = _mapper.Map<UserRoleMappingDeleteRequestEntity>(rolesMappingDto);
            var result = await _userRoleMappingRepository.DeleteUserRoleMapping(rolesEntity);

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
