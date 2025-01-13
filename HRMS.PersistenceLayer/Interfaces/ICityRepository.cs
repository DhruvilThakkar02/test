using HRMS.Entities.Address.City.CityRequestEntities;
using HRMS.Entities.Address.City.CityResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityReadResponseEntity>> GetCities();
        Task<CityReadResponseEntity?> GetCityById(int? cityId);
        Task<CityCreateResponseEntity> CreateCity(CityCreateRequestEntity city);
        Task<CityUpdateResponseEntity?> UpdateCity(CityUpdateRequestEntity city);
        Task<int> DeleteCity(CityDeleteRequestEntity city);
    }
}
