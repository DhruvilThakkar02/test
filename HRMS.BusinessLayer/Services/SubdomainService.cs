using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Subdomain.SubdomainRequestDto;
using HRMS.Dtos.Tenant.Subdomain.SubdomainResponseDto;
using HRMS.Entities.Tenant.Subdomain.SubdomainRequestEntites;
using HRMS.Entities.Tenant.Subdomain.SubdomainResponseEntites;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class SubdomainService : ISubdomainService
    {
        private readonly ISubdomainRepository _subdomainRepository;
        private readonly IMapper _mapper;

        public SubdomainService(ISubdomainRepository subdomainRepository, IMapper mapper)
        {
            _subdomainRepository = subdomainRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubdomainReadResponseDto>> GetSubdomains()
        {
            var subdomains = await _subdomainRepository.GetSubdomains();
            var response = _mapper.Map<IEnumerable<SubdomainReadResponseDto>>(subdomains);
            return response;
        }

        public async Task<SubdomainReadResponseDto?> GetSubdomainById(int? subdomainId)
        {
            var subdomain = await _subdomainRepository.GetSubdomainById(subdomainId);
            if (subdomain == null || subdomain.SubdomainId == -1)
            {
                return null;
            }
            var response = _mapper.Map<SubdomainReadResponseDto>(subdomain);
            return response;
        }

        public async Task<SubdomainCreateResponseDto> CreateSubdomain(SubdomainCreateRequestDto subdomainDto)
        {
            var subdomainEntity = _mapper.Map<SubdomainCreateRequestEntity>(subdomainDto);
            var addedSubdomain = await _subdomainRepository.CreateSubdomain(subdomainEntity);
            var response = _mapper.Map<SubdomainCreateResponseDto>(addedSubdomain);
            return response;
        }

        public async Task<SubdomainUpdateResponseDto> UpdateSubdomain(SubdomainUpdateRequestDto subdomainDto)
        {
            var sundomainEntity = _mapper.Map<SubdomainUpdateRequestEntity>(subdomainDto);
            var updatedSubdomain = await _subdomainRepository.UpdateSubdomain(sundomainEntity);
            var response = _mapper.Map<SubdomainUpdateResponseDto>(updatedSubdomain);
            return response;
        }

        public async Task<SubdomainDeleteResponseDto?> DeleteSubdomain(SubdomainDeleteRequestDto subdomainDto)
        {
            var subdomainEntity = _mapper.Map<SubdomainDeleteRequestEntity>(subdomainDto);
            var result = await _subdomainRepository.DeleteSubdomain(subdomainEntity);
            if (result == -1)
            {
                return null;
            }
            var responseEntity = new SubdomainDeleteResponseEntity { SubdomainId = subdomainEntity.SubdomainId };
            var responseDto = _mapper.Map<SubdomainDeleteResponseDto>(responseEntity);

            return responseDto;
        }
    }
}
