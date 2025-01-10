using HRMS.Dtos.Address.AddressType.AddressTypeRequestDtos;
using HRMS.Dtos.Address.AddressType.AddressTypeResponseDtos;
using HRMS.Dtos.User.User.UserRequestDtos;
using HRMS.Dtos.User.User.UserResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IAddressTypeService
    {
        Task<IEnumerable<AddressTypeReadResponseDto>> GetAddressTypes();
        Task<AddressTypeReadResponseDto?> GetAddressTypeById(int? addresstypeId);
        Task<AddressTypeCreateResponseDto> CreateAddressType(AddressTypeCreateRequestDto addresstypeDto);
        Task<AddressTypeUpdateResponseDto> UpdateAddressType(AddressTypeUpdateRequestDto addresstypeDto);
        Task<AddressTypeDeleteResponseDto?> DeleteAddressType(AddressTypeDeleteRequestDto addresstypeDto);
    }
}
