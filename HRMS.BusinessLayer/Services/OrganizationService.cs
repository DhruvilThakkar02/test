using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationResponseDtos;
using HRMS.Entities.Tenant.Organization.OrganizationRequestEntities;
using HRMS.Entities.Tenant.Organization.OrganizationResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public OrganizationService(IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<List<OrganizationReadResponseDto>> GetOrganizations()
        {
            var organizations = await _organizationRepository.GetOrganizations();
            if (organizations == null || !organizations.Any())
            {
                return new List<OrganizationReadResponseDto>();
            }
            var organizationDtos = _mapper.Map<List<OrganizationReadResponseDto>>(organizations);
            return organizationDtos;
        }

        public async Task<OrganizationReadResponseDto?> GetOrganizationById(int? id)
        {
            var organization = await _organizationRepository.GetOrganizationById(id);
            if (organization == null)
            {
                return null;
            }

            var response = _mapper.Map<OrganizationReadResponseDto>(organization);
            return response;
        }

        public async Task<OrganizationCreateResponseDto> CreateOrganization(OrganizationCreateRequestDto dto)
        {
            var organizations = _mapper.Map<OrganizationCreateRequestEntity>(dto);
            var createdOrganization = await _organizationRepository.CreateOrganization(organizations);
            return _mapper.Map<OrganizationCreateResponseDto>(createdOrganization);
        }

        public async Task<OrganizationUpdateResponseDto> UpdateOrganization(OrganizationUpdateRequestDto dto)
        {
            var organizationEntity = _mapper.Map<OrganizationUpdateRequestEntity>(dto);
            var organizations = await _organizationRepository.UpdateOrganization(organizationEntity);
            var response = _mapper.Map<OrganizationUpdateResponseDto>(organizations);
            return response;
        }

        public async Task<OrganizationDeleteResponseDto?> DeleteOrganization(OrganizationDeleteRequestDto dto)
        {
            var organizationEntity = _mapper.Map<OrganizationDeleteRequestEntity>(dto);
            var result = await _organizationRepository.DeleteOrganization(organizationEntity);

            if (result == -1)
            {
                return null;
            }
            var responseEntity = new OrganizationDeleteResponseEntity { OrganizationId = organizationEntity.OrganizationId };
            var responseDto = _mapper.Map<OrganizationDeleteResponseDto>(responseEntity);
            return responseDto;
        }
    }
}





