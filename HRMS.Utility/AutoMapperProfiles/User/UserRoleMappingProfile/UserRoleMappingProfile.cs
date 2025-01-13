using AutoMapper;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingRequestDtos;
using HRMS.Dtos.User.UserRoleMapping.UserRoleMappingResponseDtos;
using HRMS.Entities.User.UserRoleMapping.UserRoleMappingRequestEntities;
using HRMS.Entities.User.UserRoleMapping.UserRoleMappingResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.User.UserRoleMappingProfile
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
