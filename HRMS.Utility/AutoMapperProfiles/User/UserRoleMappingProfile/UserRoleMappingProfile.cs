using AutoMapper;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.User.UserRolesMappingProfile
{
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<UserRoleMappingCreateRequestDto, UserRoleMappingCreateRequestEntity>();
            CreateMap<UserRoleMappingReadRequestDto, UserRoleMappingReadRequestEntity>();
            CreateMap<UserRoleMappingUpdateRequestDto, UserRoleMappingUpdateRequestEntity>();
            CreateMap<UserRoleMappingDeleteRequestDto, UserRoleMappingDeleteRequestEntity>();

            CreateMap<UserRoleMappingCreateRequestEntity, UserRoleMappingCreateResponseEntity>();
            CreateMap<UserRoleMappingReadRequestEntity, UserRoleMappingReadResponseEntity>();
            CreateMap<UserRoleMappingUpdateRequestEntity, UserRoleMappingUpdateResponseEntity>();
            CreateMap<UserRoleMappingDeleteRequestEntity, UserRoleMappingDeleteResponseEntity>();

            CreateMap<UserRoleMappingCreateResponseEntity, UserRoleMappingCreateResponseDto>();
            CreateMap<UserRoleMappingReadResponseEntity, UserRoleMappingReadResponseDto>();
            CreateMap<UserRoleMappingUpdateResponseEntity, UserRoleMappingUpdateResponseDto>();
            CreateMap<UserRoleMappingDeleteResponseEntity, UserRoleMappingDeleteResponseDto>();
        }
    }
}
