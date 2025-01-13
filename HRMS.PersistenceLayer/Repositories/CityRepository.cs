using Dapper;
using HRMS.Entities.Address.City.CityRequestEntities;
using HRMS.Entities.Address.City.CityResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.Address;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbConnection _dbConnection;

        public CityRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CityReadResponseEntity>> GetCities()
        {
            var cities = await _dbConnection.QueryAsync<CityReadResponseEntity>(
                CityMappingStoreProcedure.GetCities,
                commandType: CommandType.StoredProcedure
            );
            return cities;
        }

        public async Task<CityReadResponseEntity?> GetCityById(int? cityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", cityId);

            var city = await _dbConnection.QueryFirstOrDefaultAsync<CityReadResponseEntity>(
                CityMappingStoreProcedure.GetCityById,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return city;
        }

        public async Task<CityCreateResponseEntity> CreateCity(CityCreateRequestEntity city)
        {
            var parameters = new DynamicParameters();
             parameters.Add("@CityId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@CityName", city.CityName);
            parameters.Add("@CreatedBy", city.CreatedBy);
           
            parameters.Add("@IsActive", city.IsActive);
              

            var result = await _dbConnection.QuerySingleOrDefaultAsync<dynamic>(
                CityMappingStoreProcedure.CreateCity,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var cityId = parameters.Get<int>("@CityId");
            var createdCity = new CityCreateResponseEntity
            {
                CityId = cityId,
                CityName = city.CityName,
                CreatedBy = city.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedBy = result?.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = city.IsActive,
                IsDelete = result?.IsDelete,
                
            };

            return createdCity;
        }

        public async Task<CityUpdateResponseEntity?> UpdateCity(CityUpdateRequestEntity city)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", city.CityId);
            parameters.Add("@CityName", city.CityName);
            parameters.Add("@UpdatedBy", city.UpdatedBy);
            parameters.Add("@IsActive", city.IsActive);
            parameters.Add("@IsDelete", city.IsDelete);
           

            var result = await _dbConnection.QuerySingleOrDefaultAsync<CityUpdateResponseEntity>(
                CityMappingStoreProcedure.UpdateCity,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (result == null || result.CityId == -1)
            {
                return null;
            }

            var updatedCity = new CityUpdateResponseEntity
            {
                CityId = city.CityId,
                CityName = city.CityName,
                CreatedBy = result.CreatedBy,
                CreatedAt = result.CreatedAt,
                UpdatedBy = city.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = city.IsActive,
                IsDelete = city.IsDelete,
                
            };

            return updatedCity;
        }

        public async Task<int> DeleteCity(CityDeleteRequestEntity city)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", city.CityId);
            var response = await _dbConnection.ExecuteScalarAsync<int>(
                CityMappingStoreProcedure.DeleteCity,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return response;
        }
    }
}

