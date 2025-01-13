using AutoMapper;
using HRMS.Dtos.Address.AddressType.AddressTypeRequestDtos;
using HRMS.Dtos.Address.AddressType.AddressTypeResponseDtos;
using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Entities.Address.AddressType.AddressTypeRequestEntities;
using HRMS.Entities.Address.AddressType.AddressTypeResponseEntities;
using HRMS.Entities.User.User.UserRequestEntities;
using HRMS.Entities.User.User.UserResponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utility.AutoMapperProfiles.Address.AddressTypeMapping
{
    public class AddressTypeMappingProfile:Profile
    {
        public AddressTypeMappingProfile()
        {
            CreateMap<AddressTypeCreateRequestDto, AddressTypeCreateRequestEntity>();
            CreateMap<AddressTypeReadRequestDto, AddressTypeReadRequestEntity>();
            CreateMap<AddressTypeUpdateRequestDto, AddressTypeUpdateRequestEntity>();
            CreateMap<AddressTypeDeleteRequestDto, AddressTypeDeleteRequestEntity>();

            CreateMap<AddressTypeCreateRequestEntity, AddressTypeCreateResponseEntity>();
            CreateMap<AddressTypeReadRequestEntity, AddressTypeReadResponseEntity>();
            CreateMap<AddressTypeUpdateRequestEntity, AddressTypeUpdateResponseEntity>();
            CreateMap<AddressTypeDeleteRequestEntity, AddressTypeDeleteResponseEntity>();

            CreateMap<AddressTypeCreateResponseEntity, AddressTypeCreateResponseDto>();
            CreateMap<AddressTypeReadResponseEntity, AddressTypeReadResponseDto>();
            CreateMap<AddressTypeUpdateResponseEntity,AddressTypeUpdateResponseDto>();
            CreateMap<AddressTypeDeleteResponseEntity, AddressTypeDeleteResponseDto>();
        }
    }
}
