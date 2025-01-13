using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.City.CityRequestDtos;
using HRMS.Dtos.Address.City.CityResponseDtos;
using HRMS.Entities.Address.City.CityRequestEntities;
using HRMS.Entities.Address.City.CityResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<List<CityReadResponseDto>> GetCities()
        {
            var cities = await _cityRepository.GetCities();
            if (cities == null || !cities.Any())
            {
                return new List<CityReadResponseDto>();
            }

            var cityDtos = _mapper.Map<List<CityReadResponseDto>>(cities);
            return cityDtos;
        }

        public async Task<CityReadResponseDto?> GetCityById(int? id)
        {
            var city = await _cityRepository.GetCityById(id);
            if (city == null)
            {
                return null;
            }

            var response = _mapper.Map<CityReadResponseDto>(city);
            return response;
        }

        public async Task<CityCreateResponseDto> CreateCity(CityCreateRequestDto dto)
        {
            var cityEntity = _mapper.Map<CityCreateRequestEntity>(dto);
            var createdCity = await _cityRepository.CreateCity(cityEntity);
            return _mapper.Map<CityCreateResponseDto>(createdCity);
        }

        public async Task<CityUpdateResponseDto> UpdateCity(CityUpdateRequestDto dto)
        {
            var cityEntity = _mapper.Map<CityUpdateRequestEntity>(dto);
            var updatedCity = await _cityRepository.UpdateCity(cityEntity);
            var response = _mapper.Map<CityUpdateResponseDto>(updatedCity);
            return response;
        }

        public async Task<CityDeleteResponseDto?> DeleteCity(CityDeleteRequestDto dto)
        {
            var cityEntity = _mapper.Map<CityDeleteRequestEntity>(dto);
            var result = await _cityRepository.DeleteCity(cityEntity);

            if (result == -1) 
            {
                return null;
            }

            var responseEntity = new CityDeleteResponseEntity { CityId = cityEntity.CityId };
            var responseDto = _mapper.Map<CityDeleteResponseDto>(responseEntity);
            return responseDto;
        }
    }
}

