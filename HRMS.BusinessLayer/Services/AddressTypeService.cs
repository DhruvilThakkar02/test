using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.AddressType.AddressTypeRequestDtos;
using HRMS.Dtos.Address.AddressType.AddressTypeResponseDtos;
using HRMS.Entities.Address.AddressType.AddressTypeRequestEntities;
using HRMS.Entities.Address.AddressType.AddressTypeResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.PersistenceLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BusinessLayer.Services
{
    public class AddressTypeService:IAddressTypeService
    {
        private readonly IAddressTypeRepository _addresstypeRepository;
        private readonly IMapper _mapper;

        public AddressTypeService(IAddressTypeRepository addresstypeRepository, IMapper mapper)
        {
            _addresstypeRepository = addresstypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressTypeReadResponseDto>> GetAddressTypes()
        {
            var addresstypes = await _addresstypeRepository.GetAddressTypes();
            
            var response = _mapper.Map<IEnumerable<AddressTypeReadResponseDto>>(addresstypes);
            return response;
        }

        public async Task<AddressTypeReadResponseDto?> GetAddressTypeById(int? addresstypeId)
        {
            var addresstype = await _addresstypeRepository.GetAddressTypeById(addresstypeId);
            if (addresstype == null || addresstype.AddressTypeId == -1)
            {
                return null;
            }

           
            var response = _mapper.Map<AddressTypeReadResponseDto>(addresstype);
            return response;
        }

        public async Task<AddressTypeCreateResponseDto> CreateAddressType(AddressTypeCreateRequestDto addresstypeDto)
        {
            var addresstypeEntity = _mapper.Map<AddressTypeCreateRequestEntity>(addresstypeDto);
            var addedAddressType = await _addresstypeRepository.CreateAddressType(addresstypeEntity);
            var response = _mapper.Map<AddressTypeCreateResponseDto>(addedAddressType);
            return response;
        }

        public async Task<AddressTypeUpdateResponseDto> UpdateAddressType(AddressTypeUpdateRequestDto addresstypeDto)
        {
            var addresstypeEntity = _mapper.Map<AddressTypeUpdateRequestEntity>(addresstypeDto);
            var updatedAddressType = await _addresstypeRepository.UpdateAddressType(addresstypeEntity);
            var response = _mapper.Map<AddressTypeUpdateResponseDto>(updatedAddressType);
            return response;
        }

        public async Task<AddressTypeDeleteResponseDto?> DeleteAddressType(AddressTypeDeleteRequestDto addresstypeDto)
        {
            var addresstypeEntity = _mapper.Map<AddressTypeDeleteRequestEntity>(addresstypeDto);
            var result = await _addresstypeRepository.DeleteAddressType(addresstypeEntity);
            if (result == -1)
            {
                return null;
            }
            var responseEntity = new AddressTypeDeleteResponseEntity { AddressTypeId = addresstypeEntity.AddressTypeId };
            var responseDto = _mapper.Map<AddressTypeDeleteResponseDto>(responseEntity);

            return responseDto;
        }
    }
}
