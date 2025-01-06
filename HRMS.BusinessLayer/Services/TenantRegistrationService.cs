using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.TenantRegistration.TenantRegistrationRequestDtos;
using HRMS.Dtos.Tenant.TenantRegistration.TenantRegistrationResponseDtos;
using HRMS.Entities.Tenant.TenantRegistration.TenantRegistrationRequestEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class TenantRegistrationService : ITenantRegistrationService
    {
        private readonly ITenantRegistrationRepository _tenantRegistrationRepository;
        private readonly IMapper _mapper;

        public TenantRegistrationService(ITenantRegistrationRepository tenantRegistrationRepository, IMapper mapper)
        {
            _tenantRegistrationRepository = tenantRegistrationRepository;
            _mapper = mapper;
        }

        public async Task<TenantRegistrationCreateResponseDto> CreateTenantRegistration(TenantRegistrationCreateRequestDto tenantRegistrationDto)
        {
            var tenantRegisterationEntity = _mapper.Map<TenantRegistrationCreateRequestEntity>(tenantRegistrationDto);
            var addedtenantRegisteration = await _tenantRegistrationRepository.CreateTenantRegistration(tenantRegisterationEntity);
            var response = _mapper.Map<TenantRegistrationCreateResponseDto>(addedtenantRegisteration);
            return response;
        }
    }
}
