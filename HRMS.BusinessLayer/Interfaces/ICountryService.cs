using HRMS.Dtos.Address.Country.CountryRequestDtos;
using HRMS.Dtos.Address.Country.CountryResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryReadResponseDto>> GetCountries();
        Task<CountryReadResponseDto?> GetCountryById(int? id);
        Task<CountryCreateResponseDto> CreateCountry(CountryCreateRequestDto dto);
        Task<CountryUpdateResponseDto> UpdateCountry(CountryUpdateRequestDto dto);
        Task<CountryDeleteResponseDto?> DeleteCountry(CountryDeleteRequestDto dto);
    }
}
