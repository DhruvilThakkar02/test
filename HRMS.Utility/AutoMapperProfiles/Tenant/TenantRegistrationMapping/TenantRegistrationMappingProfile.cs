using AutoMapper;
using HRMS.Dtos.Tenant.TenantRegistration.TenantRegistrationRequestDtos;
using HRMS.Dtos.Tenant.TenantRegistration.TenantRegistrationResponseDtos;
using HRMS.Entities.Tenant.TenantRegistration.TenantRegistrationRequestEntities;
using HRMS.Entities.Tenant.TenantRegistration.TenantRegistrationResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.Tenant.TenantRegistrationMapping
{
    public class TenantRegistrationMappingProfile : Profile
    {
        public TenantRegistrationMappingProfile()
        {

            CreateMap<TenantRegistrationCreateRequestDto, TenantRegistrationCreateRequestEntity>();


            CreateMap<TenantRegistrationCreateRequestEntity, TenantRegistrationCreateResponseEntity>();


            CreateMap<TenantRegistrationCreateResponseEntity, TenantRegistrationCreateResponseDto>();
        }
    }
}
