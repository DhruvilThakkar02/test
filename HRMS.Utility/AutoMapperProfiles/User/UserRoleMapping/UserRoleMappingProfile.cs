using AutoMapper;
using HRMS.Dtos.User.UserRoles.UserRolesRequestDtos;
using HRMS.Dtos.User.UserRoles.UserRolesResponseDtos;
using HRMS.Entities.User.UserRoles.UserRolesRequestEntities;
using HRMS.Entities.User.UserRoles.UserRolesResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.User.UserRolesMapping
{
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<UserRoleCreateRequestDto, UserRoleCreateRequestEntity>();
            CreateMap<UserRoleReadRequestDto, UserRoleReadRequestEntity>();
            CreateMap<UserRoleUpdateRequestDto, UserRoleUpdateRequestEntity>();
            CreateMap<UserRoleDeleteRequestDto, UserRoleDeleteRequestEntity>();

            CreateMap<UserRoleCreateRequestEntity, UserRoleCreateResponseEntity>();
            CreateMap<UserRoleReadRequestEntity, UserRoleReadResponseEntity>();
            CreateMap<UserRoleUpdateRequestEntity, UserRoleUpdateResponseEntity>();
            CreateMap<UserRoleDeleteRequestEntity, UserRoleDeleteResponseEntity>();

            CreateMap<UserRoleCreateResponseEntity, UserRoleCreateResponseDto>();
            CreateMap<UserRoleReadResponseEntity, UserRoleReadResponseDto>();
            CreateMap<UserRoleUpdateResponseEntity, UserRoleUpdateResponseDto>();
            CreateMap<UserRoleDeleteResponseEntity, UserRoleDeleteResponseDto>();
        }
    }
}
