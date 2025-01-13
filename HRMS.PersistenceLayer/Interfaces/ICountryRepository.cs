using HRMS.Entities.Address.Country.CountryRequestEntities;
using HRMS.Entities.Address.Country.CountryResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryReadResponseEntity>> GetCountries();
        Task<CountryReadResponseEntity?> GetCountryById(int? countryId);
        Task<CountryCreateResponseEntity> CreateCountry(CountryCreateRequestEntity country);
        Task<CountryUpdateResponseEntity?> UpdateCountry(CountryUpdateRequestEntity country);
        Task<int> DeleteCountry(CountryDeleteRequestEntity country);
    }
}
