using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using HRMS.Entities.User.UserRoles.UserRolesRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessLayer.Services
{
    public class UserRolesMappingService : IUserRolesMappingService
    {
        private readonly IMapper _mapper;
        private readonly IUserRolesMappingRepository _userRolesMappingRepository;
        public UserRolesMappingService(IMapper mapper,IUserRolesMappingRepository userRolesMappingRepository) 
        {
          
            _mapper = mapper;
            _userRolesMappingRepository = userRolesMappingRepository;
        }

        public async Task<IEnumerable<UserRolesMappingReadResponseDto>> GetAllUserRolesMapping()
        {
            var mappingroles = await _userRolesMappingRepository.GetAllUserRolesMapping();
            var response = _mapper.Map<IEnumerable<UserRolesMappingReadResponseDto>>(mappingroles);
            return response;
        }

        public async Task<UserRolesMappingReadResponseDto?> GetByIdUserRolesMapping(int? roleid)
        {
            var role = await _userRolesMappingRepository.GetByIdUserRolesMapping(roleid);
            if (role == null || role.UserRoleMappingId == -1)
            {
                return null;
            }
            var response =  _mapper.Map<UserRolesMappingReadResponseDto>(role);
            return response;
        }
        public async Task<UserRolesMappingCreateResponseDto> CreateUserRolesMapping(UserRolesMappingCreateRequestDto rolesMappingDto)
        {
            var rolesMappingEntity = _mapper.Map<UserRolesMappingCreateRequestEntity>(rolesMappingDto);
            var addedUserMappingRole = await _userRolesMappingRepository.CreateUserRolesMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRolesMappingCreateResponseDto>(addedUserMappingRole);
            return response;
        }
        public async Task<UserRolesMappingUpdateResponseDto?> UpdateUserRolesMapping(UserRolesMappingUpdateRequestDto rolesMappingDto)
        {
            var rolesMappingEntity = _mapper.Map<UserRolesMappingUpdateRequestEntity>(rolesMappingDto);
            var UpdatedMappingRole = await _userRolesMappingRepository.UpdateUserRolesMapping(rolesMappingEntity);
            var response = _mapper.Map<UserRolesMappingUpdateResponseDto>(UpdatedMappingRole);
            return response;
        }

    }
}
