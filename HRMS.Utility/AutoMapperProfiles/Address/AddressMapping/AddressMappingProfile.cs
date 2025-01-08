using AutoMapper;
using HRMS.Dtos.Address.Address.AddressRequestDtos;
using HRMS.Dtos.Address.Address.AddressResponseDtos;
using HRMS.Entities.Address.Address.AddressRequestEntities;
using HRMS.Entities.Address.Address.AddressResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.Address.AddressMapping
{
    public  class AddressMappingProfile:Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<AddressCreateRequestDto, AddressCreateRequestEntity>();
            CreateMap<AddressReadRequestDto, AddressReadRequestEntity>();
            CreateMap<AddressUpdateRequestDto, AddressUpdateRequestEntity>();
            CreateMap<AddressDeleteRequestDto, AddressDeleteRequestEntity>();

            CreateMap<AddressCreateRequestEntity, AddressCreateResponseEntity>();
            CreateMap<AddressReadRequestEntity, AddressReadResponseEntity>();
            CreateMap<AddressUpdateRequestEntity, AddressUpdateResponseEntity>();
            CreateMap<AddressDeleteRequestEntity, AddressDeleteResponseEntity>();

            CreateMap<AddressCreateResponseEntity, AddressCreateResponseDto>();
            CreateMap<AddressReadResponseEntity, AddressReadResponseDto>();
            CreateMap<AddressUpdateResponseEntity, AddressUpdateResponseDto>();
            CreateMap<AddressDeleteResponseEntity, AddressDeleteResponseDto>();
        }
    }
}
