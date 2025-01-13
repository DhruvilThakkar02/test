using AutoMapper;
using HRMS.Dtos.Address.City.CityRequestDtos;
using HRMS.Dtos.Address.City.CityResponseDtos;
using HRMS.Entities.Address.City.CityRequestEntities;
using HRMS.Entities.Address.City.CityResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.Address.City
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<CityCreateRequestDto, CityCreateRequestEntity>();
            CreateMap<CityReadRequestDto, CityReadRequestEntity>();
            CreateMap<CityUpdateRequestDto, CityUpdateRequestEntity>();
            CreateMap<CityDeleteRequestDto, CityDeleteRequestEntity>();

            CreateMap<CityCreateRequestEntity, CityCreateResponseEntity>();
            CreateMap<CityReadRequestEntity, CityReadResponseEntity>();
            CreateMap<CityUpdateRequestEntity, CityUpdateResponseEntity>();
            CreateMap<CityDeleteRequestEntity, CityDeleteResponseEntity>();

            CreateMap<CityCreateResponseEntity, CityCreateResponseDto>();
            CreateMap<CityReadResponseEntity, CityReadResponseDto>();
            CreateMap<CityUpdateResponseEntity, CityUpdateResponseDto>();
            CreateMap<CityDeleteResponseEntity, CityDeleteResponseDto>();
        }
    }
}
