using HRMS.Dtos.Address.State.StateRequestDtos;
using HRMS.Dtos.Address.State.StateResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface IStateService
    {
        Task<List<StateReadResponseDto>> GetStates();
        Task<StateReadResponseDto?> GetStateById(int? id);
        Task<StateCreateResponseDto> CreateState(StateCreateRequestDto dto);
        Task<StateUpdateResponseDto> UpdateState(StateUpdateRequestDto dto);
        Task<StateDeleteResponseDto?> DeleteState(StateDeleteRequestDto dto);
    }
}
