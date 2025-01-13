using Dapper;
using HRMS.Entities.Address.Country.CountryRequestEntities;
using HRMS.Entities.Address.Country.CountryResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.Address;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IDbConnection _dbConnection;

        public CountryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CountryReadResponseEntity>> GetCountries()
        {
            var countries = await _dbConnection.QueryAsync<CountryReadResponseEntity>(CountryMappingStoreProcedure.GetCountries,commandType: CommandType.StoredProcedure);
            return countries;
        }

        public async Task<CountryReadResponseEntity?> GetCountryById(int? countryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", countryId);

            var country = await _dbConnection.QueryFirstOrDefaultAsync<CountryReadResponseEntity>(CountryMappingStoreProcedure.GetCountryById,parameters,commandType: CommandType.StoredProcedure);
            return country;
        }

        public async Task<CountryCreateResponseEntity> CreateCountry(CountryCreateRequestEntity country)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@CountryName", country.CountryName);
            parameters.Add("@CreatedBy", country.CreatedBy);
            parameters.Add("@IsActive", country.IsActive);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<CountryReadResponseEntity>(CountryMappingStoreProcedure.CreateCountry,parameters, commandType: CommandType.StoredProcedure);

            var countryId = parameters.Get<int>("@CountryId");
            var createdCountry = new CountryCreateResponseEntity
            {
                CountryId = countryId,
                CountryName = country.CountryName,
                CreatedBy = country.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = country.IsActive,
            };

            return createdCountry;
        }

        public async Task<CountryUpdateResponseEntity?> UpdateCountry(CountryUpdateRequestEntity country)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", country.CountryId);
            parameters.Add("@CountryName", country.CountryName);
            parameters.Add("@UpdatedBy", country.UpdatedBy);
            parameters.Add("@IsActive", country.IsActive);
            parameters.Add("@IsDelete", country.IsDelete);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<CountryUpdateResponseEntity>(CountryMappingStoreProcedure.UpdateCountry,parameters,commandType: CommandType.StoredProcedure);

            if (result == null || result.CountryId == -1)
            {
                return null;
            }

            var updatedCountry = new CountryUpdateResponseEntity
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                UpdatedBy = country.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = country.IsActive,
                IsDelete = country.IsDelete
            };

            return updatedCountry;
        }

        public async Task<int> DeleteCountry(CountryDeleteRequestEntity country)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", country.CountryId);
            var response = await _dbConnection.ExecuteScalarAsync<int>(CountryMappingStoreProcedure.DeleteCountry,parameters, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}

