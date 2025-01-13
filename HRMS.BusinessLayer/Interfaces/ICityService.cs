using HRMS.Dtos.Address.City.CityRequestDtos;
using HRMS.Dtos.Address.City.CityResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface ICityService
    {
        Task<List<CityReadResponseDto>> GetCities();
        Task<CityReadResponseDto?> GetCityById(int? id);
        Task<CityCreateResponseDto> CreateCity(CityCreateRequestDto dto);
        Task<CityUpdateResponseDto> UpdateCity(CityUpdateRequestDto dto);
        Task<CityDeleteResponseDto?> DeleteCity(CityDeleteRequestDto dto);
    }
}
