using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Address.State.StateRequestDtos;
using HRMS.Dtos.Address.State.StateResponseDtos;
using HRMS.Entities.Address.State.StateRequestEntities;
using HRMS.Entities.Address.State.StateResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public StateService(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<List<StateReadResponseDto>> GetStates()
        {
            var states = await _stateRepository.GetStates();
            if (states == null || !states.Any())
            {
                return new List<StateReadResponseDto>();
            }

            var stateDtos = _mapper.Map<List<StateReadResponseDto>>(states);
            return stateDtos;
        }

        public async Task<StateReadResponseDto?> GetStateById(int? id)
        {
            var state = await _stateRepository.GetStateById(id);
            if (state == null)
            {
                return null;
            }

            var response = _mapper.Map<StateReadResponseDto>(state);
            return response;
        }

        public async Task<StateCreateResponseDto> CreateState(StateCreateRequestDto dto)
        {
            var stateEntity = _mapper.Map<StateCreateRequestEntity>(dto);
            var createdState = await _stateRepository.CreateState(stateEntity);
            return _mapper.Map<StateCreateResponseDto>(createdState);
        }

        public async Task<StateUpdateResponseDto> UpdateState(StateUpdateRequestDto dto)
        {
            var stateEntity = _mapper.Map<StateUpdateRequestEntity>(dto);
            var updatedState = await _stateRepository.UpdateState(stateEntity);
            var response = _mapper.Map<StateUpdateResponseDto>(updatedState);
            return response;
        }

        public async Task<StateDeleteResponseDto?> DeleteState(StateDeleteRequestDto dto)
        {
            var stateEntity = _mapper.Map<StateDeleteRequestEntity>(dto);
            var result = await _stateRepository.DeleteState(stateEntity);

            if (result == -1)  
            {
                return null;
            }

            var responseEntity = new StateDeleteResponseEntity { StateId = stateEntity.StateId };
            var responseDto = _mapper.Map<StateDeleteResponseDto>(responseEntity);
            return responseDto;
        }
    }
}

