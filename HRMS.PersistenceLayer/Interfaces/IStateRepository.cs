using HRMS.Entities.Address.State.StateRequestEntities;
using HRMS.Entities.Address.State.StateResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IStateRepository
    {
        Task<IEnumerable<StateReadResponseEntity>> GetStates();
        Task<StateReadResponseEntity?> GetStateById(int? stateId);
        Task<StateCreateResponseEntity> CreateState(StateCreateRequestEntity state);
        Task<StateUpdateResponseEntity?> UpdateState(StateUpdateRequestEntity state);
        Task<int> DeleteState(StateDeleteRequestEntity state);
    }
}
