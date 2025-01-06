using AutoMapper;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingRequestDtos;
using HRMS.Dtos.User.UserRolesMapping.UserRolesMappingResponseDtos;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingRequestEntities;
using HRMS.Entities.User.UserRolesMapping.UserRolesMappingResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.User.UserRolesMappingProfile
{
    public class UserRolesMappingProfile : Profile
    {
        public UserRolesMappingProfile()
        {
            CreateMap<UserRolesMappingCreateRequestDto, UserRolesMappingCreateRequestEntity>();
            CreateMap<UserRolesMappingReadRequestDto, UserRolesMappingReadRequestEntity>();
            CreateMap<UserRolesMappingUpdateRequestDto, UserRolesMappingUpdateRequestEntity>();
            CreateMap<UserRolesMappingDeleteRequestDto, UserRolesMappingDeleteRequestEntity>();

            CreateMap<UserRolesMappingCreateRequestEntity, UserRolesMappingCreateResponseEntity>();
            CreateMap<UserRolesMappingReadRequestEntity, UserRolesMappingReadResponseEntity>();
            CreateMap<UserRolesMappingUpdateRequestEntity, UserRolesMappingUpdateResponseEntity>();
            CreateMap<UserRolesMappingDeleteRequestEntity, UserRolesMappingDeleteResponseEntity>();

            CreateMap<UserRolesMappingCreateResponseEntity, UserRolesMappingCreateResponseDto>();
            CreateMap<UserRolesMappingReadResponseEntity, UserRolesMappingReadResponseDto>();
            CreateMap<UserRolesMappingUpdateResponseEntity, UserRolesMappingUpdateResponseDto>();
            CreateMap<UserRolesMappingDeleteResponseEntity, UserRolesMappingDeleteResponseDto>();
        }
    }
}
