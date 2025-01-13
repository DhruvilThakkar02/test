using Dapper;
using HRMS.Entities.Address.State.StateRequestEntities;
using HRMS.Entities.Address.State.StateResponseEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.Utility.Helpers.SqlHelpers.Address;
using System.Data;

namespace HRMS.PersistenceLayer.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly IDbConnection _dbConnection;

        public StateRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<StateReadResponseEntity>> GetStates()
        {
            var states = await _dbConnection.QueryAsync<StateReadResponseEntity>(
                StateMappingStoreProcedure.GetStates,
                commandType: CommandType.StoredProcedure
            );
            return states;
        }

        public async Task<StateReadResponseEntity?> GetStateById(int? stateId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StateId", stateId);

            var state = await _dbConnection.QueryFirstOrDefaultAsync<StateReadResponseEntity>(
                StateMappingStoreProcedure.GetStateById,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return state;
        }

        public async Task<StateCreateResponseEntity> CreateState(StateCreateRequestEntity state)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StateName", state.StateName);
            parameters.Add("@IsActive", state.IsActive);
            parameters.Add("@CreatedBy", state.CreatedBy);
            parameters.Add("@StateId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _dbConnection.QuerySingleOrDefaultAsync<dynamic>(
                StateMappingStoreProcedure.CreateState,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var stateId = parameters.Get<int>("@StateId");

            var createdState = new StateCreateResponseEntity
            {
                StateId = stateId,
                StateName = state.StateName,
                IsActive = state.IsActive,
                CreatedBy = state.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return createdState;
        }


        public async Task<StateUpdateResponseEntity?> UpdateState(StateUpdateRequestEntity state)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StateId", state.StateId);
            parameters.Add("@StateName", state.StateName);
            parameters.Add("@IsActive", state.IsActive);
            parameters.Add("@UpdatedBy", state.UpdatedBy);


            var result = await _dbConnection.QuerySingleOrDefaultAsync<StateUpdateResponseEntity>(
                StateMappingStoreProcedure.UpdateState,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (result == null || result.StateId == -1)
            {
                return null;
            }

            var updatedState = new StateUpdateResponseEntity
            {
                StateId = state.StateId,
                StateName = state.StateName,
                IsActive = state.IsActive,
                UpdatedBy = state.UpdatedBy,
                UpdatedAt = DateTime.Now
            };

            return updatedState;
        }

        public async Task<int> DeleteState(StateDeleteRequestEntity state)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StateId", state.StateId);
            var result = await _dbConnection.ExecuteScalarAsync<int>(
                StateMappingStoreProcedure.DeleteState,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}

