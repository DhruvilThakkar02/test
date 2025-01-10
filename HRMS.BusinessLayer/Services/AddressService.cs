using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.Address.AddressRequestDtos;
using HRMS.Dtos.Address.Address.AddressResponseDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationResponseDtos;
using HRMS.Entities.Address.Address.AddressRequestEntities;
using HRMS.Entities.Address.Address.AddressResponseEntities;

using HRMS.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessLayer.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<List<AddressReadResponseDto>> GetAddresses()
        {
            var addresses = await _addressRepository.GetAddresses();
            if (addresses == null || !addresses.Any())
            {
                return new List<AddressReadResponseDto>();
            }
            var addressDtos = _mapper.Map<List<AddressReadResponseDto>>(addresses);
            return addressDtos;
        }

        public async Task<AddressReadResponseDto?> GetAddressById(int? id)
        {
            var address = await _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return null;
            }

            var response = _mapper.Map<AddressReadResponseDto>(address);
            return response;
        }

        public async Task<AddressCreateResponseDto> CreateAddress(AddressCreateRequestDto dto)
        {
            var addresses = _mapper.Map<AddressCreateRequestEntity>(dto);
            var createdAddress = await _addressRepository.CreateAddress(addresses);
            return _mapper.Map<AddressCreateResponseDto>(createdAddress);
        }

        public async Task<AddressUpdateResponseDto> UpdateAddress(AddressUpdateRequestDto dto)
        {
            var addressEntity = _mapper.Map<AddressUpdateRequestEntity>(dto);
            var addresses = await _addressRepository.UpdateAddress(addressEntity);
            var response = _mapper.Map<AddressUpdateResponseDto>(addresses);
            return response;
        }

        public async Task<AddressDeleteResponseDto?> DeleteAddress(AddressDeleteRequestDto dto)
        {
            var addressEntity = _mapper.Map<AddressDeleteRequestEntity>(dto);
            var result = await _addressRepository.DeleteAddress(addressEntity);

            if (result == -1)
            {
                return null;
            }
            var responseEntity = new AddressDeleteResponseEntity { AddressId = addressEntity.AddressId };
            var responseDto = _mapper.Map<AddressDeleteResponseDto>(responseEntity);
            return responseDto;
        }
    }
}
