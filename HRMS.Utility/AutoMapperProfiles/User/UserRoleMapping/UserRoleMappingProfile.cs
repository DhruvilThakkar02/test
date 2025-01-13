using AutoMapper;
using HRMS.Dtos.User.UserRole.UserRoleRequestDtos;
using HRMS.Dtos.User.UserRole.UserRoleResponseDtos;
using HRMS.Entities.User.UserRole.UserRoleRequestEntities;
using HRMS.Entities.User.UserRole.UserRoleResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.User.UserRoleMapping
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
