using AutoMapper;
using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Dtos.Address.Country.CountryResponseDtos;
using HRMS.Entities.Address.Country.CountryRequestEntities;
using HRMS.Entities.Address.Country.CountryResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.Address.Country
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {

            CreateMap<CountryCreateRequestDto, CountryCreateRequestEntity>();
            CreateMap<CountryReadRequestDto, CountryReadRequestEntity>();
            CreateMap<CountryUpdateRequestDto, CountryUpdateRequestEntity>();
            CreateMap<CountryDeleteRequestDto, CountryDeleteRequestEntity>();


            CreateMap<CountryCreateRequestEntity, CountryCreateResponseEntity>();
            CreateMap<CountryReadRequestEntity, CountryReadResponseEntity>();
            CreateMap<CountryUpdateRequestEntity, CountryUpdateResponseEntity>();
            CreateMap<CountryDeleteRequestEntity, CountryDeleteResponseEntity>();


            CreateMap<CountryCreateResponseEntity, CountryCreateResponseDto>();
            CreateMap<CountryReadResponseEntity, CountryReadResponseDto>();
            CreateMap<CountryUpdateResponseEntity, CountryUpdateResponseDto>();
            CreateMap<CountryDeleteResponseEntity, CountryDeleteResponseDto>();
        }
    }
}
