using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Dtos.Address.Country.CountryResponseDtos;
using HRMS.Entities.Address.Country.CountryRequestEntities;
using HRMS.Entities.Address.Country.CountryResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<List<CountryReadResponseDto>> GetCountries()
        {
            var countries = await _countryRepository.GetCountries();
            if (countries == null || !countries.Any())
            {
                return new List<CountryReadResponseDto>();
            }

            var countryDtos = _mapper.Map<List<CountryReadResponseDto>>(countries);
            return countryDtos;
        }

        public async Task<CountryReadResponseDto?> GetCountryById(int? id)
        {
            var country = await _countryRepository.GetCountryById(id);
            if (country == null)
            {
                return null;
            }

            var response = _mapper.Map<CountryReadResponseDto>(country);
            return response;
        }

        public async Task<CountryCreateResponseDto> CreateCountry(CountryCreateRequestDto dto)
        {
            var countryEntity = _mapper.Map<CountryCreateRequestEntity>(dto);
            var createdCountry = await _countryRepository.CreateCountry(countryEntity);
            return _mapper.Map<CountryCreateResponseDto>(createdCountry);
        }


        public async Task<CountryUpdateResponseDto> UpdateCountry(CountryUpdateRequestDto dto)
        {
            var countryEntity = _mapper.Map<CountryUpdateRequestEntity>(dto);
            var updatedCountry = await _countryRepository.UpdateCountry(countryEntity);
            var response = _mapper.Map<CountryUpdateResponseDto>(updatedCountry);
            return response;
        }

        public async Task<CountryDeleteResponseDto?> DeleteCountry(CountryDeleteRequestDto dto)
        {
            var countryEntity = _mapper.Map<CountryDeleteRequestEntity>(dto);
            var result = await _countryRepository.DeleteCountry(countryEntity);

            if (result == -1)
            {
                return null;  // Failed to delete
            }

            var responseEntity = new CountryDeleteResponseEntity { CountryId = countryEntity.CountryId };
            var responseDto = _mapper.Map<CountryDeleteResponseDto>(responseEntity);
            return responseDto;
        }
    }
}

