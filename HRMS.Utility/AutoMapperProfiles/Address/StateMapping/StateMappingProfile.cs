using AutoMapper;
using HRMS.Dtos.Address.State.StateRequestDtos;
using HRMS.Dtos.Address.State.StateResponseDtos;
using HRMS.Entities.Address.State.StateRequestEntities;
using HRMS.Entities.Address.State.StateResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.Address.State
{
    public class StateMappingProfile : Profile
    {
        public StateMappingProfile()
        {

            CreateMap<StateCreateRequestDto, StateCreateRequestEntity>();
            CreateMap<StateReadRequestDto, StateReadRequestEntity>();
            CreateMap<StateUpdateRequestDto, StateUpdateRequestEntity>();
            CreateMap<StateDeleteRequestDto, StateDeleteRequestEntity>();


            CreateMap<StateCreateRequestEntity, StateCreateResponseEntity>();
            CreateMap<StateReadRequestEntity, StateReadResponseEntity>();
            CreateMap<StateUpdateRequestEntity, StateUpdateResponseEntity>();
            CreateMap<StateDeleteRequestEntity, StateDeleteResponseEntity>();


            CreateMap<StateCreateResponseEntity, StateCreateResponseDto>();
            CreateMap<StateReadResponseEntity, StateReadResponseDto>();
            CreateMap<StateUpdateResponseEntity, StateUpdateResponseDto>();
            CreateMap<StateDeleteResponseEntity, StateDeleteResponseDto>();
        }
    }
}
