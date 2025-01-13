using HRMS.Dtos.Address.Address.AddressRequestDtos;
using HRMS.Dtos.Address.Address.AddressResponseDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressReadResponseDto>> GetAddresses();
        Task<AddressReadResponseDto?> GetAddressById(int? id);
        Task<AddressCreateResponseDto> CreateAddress(AddressCreateRequestDto dto);
        Task<AddressUpdateResponseDto> UpdateAddress(AddressUpdateRequestDto dto);
        Task<AddressDeleteResponseDto?> DeleteAddress(AddressDeleteRequestDto dto);
    }
}
