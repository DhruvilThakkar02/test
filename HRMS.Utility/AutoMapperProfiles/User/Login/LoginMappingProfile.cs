using AutoMapper;
using HRMS.Dtos.User.Login.LoginRequestDtos;
using HRMS.Dtos.User.Login.LoginResponseDtos;
using HRMS.Entities.User.Login.LoginRequestEntities;
using HRMS.Entities.User.Login.LoginResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.User.Login
{
    public class LoginMappingProfile : Profile
    {
        public LoginMappingProfile()
        {
            CreateMap<TokenInformation, TokenInfo>();

            CreateMap<LoginRequestDto, LoginRequestEntity>();

            CreateMap<LoginRequestEntity, LoginResponseEntity>();

            CreateMap<LoginResponseEntity, LoginResponseDto>()
          .ForMember(dest => dest.TokenDetails, opt => opt.MapFrom(src => src.TokenDetails));
        }

    }
}
