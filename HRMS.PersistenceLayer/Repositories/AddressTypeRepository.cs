using Dapper;
using HRMS.Entities.Address.AddressType.AddressTypeRequestEntities;
using HRMS.Entities.Address.AddressType.AddressTypeResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.Address;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class AddressTypeRepository : IAddressTypeRepository
    {
        private readonly IDbConnection _dbConnection;

        public AddressTypeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<AddressTypeReadResponseEntity>> GetAddressTypes()
        {
            var addresstypes = await _dbConnection.QueryAsync<AddressTypeReadResponseEntity>(AddressTypeStoredProcedures.GetAddressTypes, commandType: CommandType.StoredProcedure);



            return addresstypes;
        }

        public async Task<AddressTypeReadResponseEntity?> GetAddressTypeById(int? addresstypeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressTypeId", addresstypeId);

            var addresstype = await _dbConnection.QueryFirstOrDefaultAsync<AddressTypeReadResponseEntity>(AddressTypeStoredProcedures.GetAddressTypeById, parameters, commandType: CommandType.StoredProcedure);
            return addresstype;
        }

        public async Task<AddressTypeCreateResponseEntity> CreateAddressType(AddressTypeCreateRequestEntity addresstype)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressTypeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@AddressTypeName", addresstype.AddressTypeName);

            parameters.Add("@IsActive", addresstype.IsActive);
            parameters.Add("@CreatedBy", addresstype.CreatedBy);


            var result = await _dbConnection.QuerySingleOrDefaultAsync<dynamic>(AddressTypeStoredProcedures.CreateAddressType, parameters, commandType: CommandType.StoredProcedure);

            var addresstypeId = parameters.Get<int>("@AddressTypeId");


            var createdAddressType = new AddressTypeCreateResponseEntity
            {
                AddressTypeId = addresstypeId,
                CreatedBy = addresstype.CreatedBy,
                CreatedAt = DateTime.Now,


                IsActive = addresstype.IsActive,
                IsDelete = result?.IsDelete
            };

            return createdAddressType;
        }

        public async Task<AddressTypeUpdateResponseEntity?> UpdateAddressType(AddressTypeUpdateRequestEntity addresstype)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressTypeId", addresstype.AddressTypeId);
            parameters.Add("@AddressTypeName", addresstype.AddressTypeName);


            parameters.Add("@IsActive", addresstype.IsActive);
            parameters.Add("@IsDelete", addresstype.IsDelete);
            parameters.Add("@UpdatedBy", addresstype.UpdatedBy);


            var result = await _dbConnection.QuerySingleOrDefaultAsync<AddressTypeUpdateResponseEntity>(AddressTypeStoredProcedures.UpdateAddressType, parameters, commandType: CommandType.StoredProcedure);

            if (result == null || result.AddressTypeId == -1)
            {
                return null;
            }



            var updatedAddressType = new AddressTypeUpdateResponseEntity
            {
                AddressTypeId = addresstype.AddressTypeId,
                AddressTypeName = addresstype.AddressTypeName,
                CreatedBy = result.CreatedBy,
                CreatedAt = result.CreatedAt,
                UpdatedBy = addresstype.UpdatedBy,
                UpdatedAt = DateTime.Now,
                IsActive = addresstype.IsActive,
                IsDelete = addresstype.IsDelete
            };

            return updatedAddressType;
        }

        public async Task<int> DeleteAddressType(AddressTypeDeleteRequestEntity addresstype)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AddressTypeId", addresstype.AddressTypeId);

            var result = await _dbConnection.ExecuteScalarAsync<int>(AddressTypeStoredProcedures.DeleteAddressType, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
