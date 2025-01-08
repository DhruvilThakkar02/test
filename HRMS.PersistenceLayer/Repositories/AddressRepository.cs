using Dapper;
using HRMS.Entities.Address.Address.AddressRequestEntities;
using HRMS.Entities.Address.Address.AddressResponseEntities;
using HRMS.Entities.User.User.UserRequestEntities;
using HRMS.Entities.User.User.UserResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.Passwords;
using HRMS.Utility.Helpers.SqlHelpers.Address;
using HRMS.Utility.Helpers.SqlHelpers.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.PersistenceLayer.Repositories
{
    public class AddressRepository:IAddressRepository
    {
        private readonly IDbConnection _dbConnection;

        public AddressRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<AddressReadResponseEntity>> GetAddresses()
        {
            var addresses = await _dbConnection.QueryAsync<AddressReadResponseEntity>(AddressStoredProcedures.GetAddresses, commandType: CommandType.StoredProcedure);

            return addresses;
        }

        public async Task<AddressReadResponseEntity?> GetAddressById(int? addressId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressId", addressId);

            var address = await _dbConnection.QueryFirstOrDefaultAsync<AddressReadResponseEntity>(AddressStoredProcedures.GetAddressById, parameters, commandType: CommandType.StoredProcedure);
            return address;
        }

        public async Task<AddressCreateResponseEntity> CreateAddress(AddressCreateRequestEntity address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@AddressLine1", address.AddressLine1);
            parameters.Add("@AddressLine2", address.AddressLine2);
            parameters.Add("@CityId", address.CityId);
            parameters.Add("@StateId", address.StateId);
            parameters.Add("@CountryId", address.CountryId);
            parameters.Add("@PostalCode", address.PostalCode);
            parameters.Add("@AddressTypeId", address.AddressTypeId);
            parameters.Add("@IsActive", address.IsActive);  
            parameters.Add("@CreatedBy", address.CreatedBy);
           

            var result = await _dbConnection.QuerySingleOrDefaultAsync<AddressCreateResponseEntity>(AddressStoredProcedures.CreateAddress, parameters, commandType: CommandType.StoredProcedure);

            var addressId = parameters.Get<int>("@AddressId");
           
            var createdAddress = new AddressCreateResponseEntity
            {
               AddressId  = addressId,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                CityId = address.CityId,
                StateId = address.StateId,
                CountryId = address.CountryId,
          
                PostalCode = address.PostalCode,
                AddressTypeId = address.AddressTypeId,
                CreatedBy = address.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedBy = result?.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = address.IsActive,
                IsDelete = result ?.IsDelete
            };

            return createdAddress;
        }

        public async Task<AddressUpdateResponseEntity?> UpdateAddress(AddressUpdateRequestEntity address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressId", address.AddressId);
            parameters.Add("@AddressLine1", address.AddressLine1);
            parameters.Add("@AddressLine2", address.AddressLine2);
            parameters.Add("@CityId", address.CityId);          
            parameters.Add("@StateId", address.StateId);
            parameters.Add("@CountryId", address.CountryId);
            parameters.Add("@PostalCode", address.PostalCode);
            parameters.Add("@AddressTypeId", address.AddressTypeId);
            parameters.Add("@IsActive", address.IsActive);
            parameters.Add("@IsDelete",address.IsDelete);
            parameters.Add("@UpdatedBy", address.UpdatedBy);
    
            var result = await _dbConnection.QuerySingleOrDefaultAsync<AddressUpdateResponseEntity>(AddressStoredProcedures.UpdateAddress, parameters, commandType: CommandType.StoredProcedure);

            if (result == null || result.AddressId == -1)
            {
                return null;
            }

           

            var updatedAddress = new AddressUpdateResponseEntity
            {
                AddressId = address.AddressId,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                CityId = address.CityId,
                StateId = address.StateId,
                CountryId = address.CountryId,
                PostalCode = address.PostalCode,
                AddressTypeId = address.AddressTypeId,
                CreatedBy = result.CreatedBy,
                CreatedAt = result.CreatedAt,
                UpdatedBy = address.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = address.IsActive,
                IsDelete = address.IsDelete
            };

            return updatedAddress;
        }

        public async Task<int> DeleteAddress(AddressDeleteRequestEntity address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressId", address.AddressId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(AddressStoredProcedures.DeleteAddress, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
