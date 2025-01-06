using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class UserRolesMappingService : IUserRolesMappingService
    {
        private readonly IMapper _mapper;
        private readonly IUserRolesMappingRepository _userRolesMappingRepository;

        public UserRolesMappingService(IMapper mapper, IUserRolesMappingRepository userRolesMappingRepository)
        {

            _mapper = mapper;
            _userRolesMappingRepository = userRolesMappingRepository;
        }

        public async Task<IEnumerable<UserRolesMappingReadResponseDto>> GetUserRolesMapping()
        {
            var mappingroles = await _userRolesMappingRepository.GetUserRolesMapping();
            var response = _mapper.Map<IEnumerable<UserRolesMappingReadResponseDto>>(mappingroles);
            return response;
        }

        public async Task<UserRolesMappingReadResponseDto?> GetUserRoleMappingById(int? roleid)
        {
            var role = await _userRolesMappingRepository.GetUserRoleMappingById(roleid);
            if (role == null || role.UserRoleMappingId == -1)
            {
                return null;
            }
            var response = _mapper.Map<UserRolesMappingReadResponseDto>(role);
            return response;
        }

        public async Task<UserRolesMappingCreateResponseDto> CreateUserRoleMapping(UserRolesMappingCreateRequestDto rolesMappingDto)
        {
            var rolesMappingEntity = _mapper.Map<UserRolesMappingCreateRequestEntity>(rolesMappingDto);
            var addedUserMappingRole = await _userRolesMappingRepository.CreateUserRoleMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRolesMappingCreateResponseDto>(addedUserMappingRole);
            return response;
        }
        public async Task<UserRolesMappingUpdateResponseDto?> UpdateUserRoleMapping(UserRolesMappingUpdateRequestDto rolesMappingDto)
        {
            var rolesMappingEntity = _mapper.Map<UserRolesMappingUpdateRequestEntity>(rolesMappingDto);
            var UpdatedMappingRole = await _userRolesMappingRepository.UpdateUserRolesMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRolesMappingUpdateResponseDto>(UpdatedMappingRole);
            return response;
        }

    }
}
