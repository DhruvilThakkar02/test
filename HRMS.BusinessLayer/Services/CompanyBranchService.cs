using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;
using HRMS.Dtos.CompanyBranch.CompanyBranchResponseDtos;
using HRMS.Entities.CompanyBranch.CompanyBranchRequestEntities;
using HRMS.Entities.CompanyBranch.CompanyBranchResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class CompanyBranchService : ICompanyBranchService
    {
        private readonly ICompanyBranchRepository _companyBranchRepository;
        private readonly IMapper _mapper;

        public CompanyBranchService(ICompanyBranchRepository companyBranchRepository, IMapper mapper)
        {
            _companyBranchRepository = companyBranchRepository;
            _mapper = mapper;
        }

        public async Task<List<CompanyBranchReadResponseDto>> GetCompanyBranches()
        {
            var companyBranches = await _companyBranchRepository.GetCompanyBranches();
            if (companyBranches == null || !companyBranches.Any())
            {
                return new List<CompanyBranchReadResponseDto>();
            }
            var companyBranchDtos = _mapper.Map<List<CompanyBranchReadResponseDto>>(companyBranches);
            return companyBranchDtos;
        }

        public async Task<CompanyBranchReadResponseDto?> GetCompanyBranchById(int? id)
        {
            var companyBranch = await _companyBranchRepository.GetCompanyBranchById(id);
            if (companyBranch == null)
            {
                return null;
            }

            var response = _mapper.Map<CompanyBranchReadResponseDto>(companyBranch);
            return response;
        }

        public async Task<CompanyBranchCreateResponseDto> CreateCompanyBranch(CompanyBranchCreateRequestDto dto)
        {
            var companyBranchEntity = _mapper.Map<CompanyBranchCreateRequestEntity>(dto);
            var createdCompanyBranch = await _companyBranchRepository.CreateCompanyBranch(companyBranchEntity);
            return _mapper.Map<CompanyBranchCreateResponseDto>(createdCompanyBranch);
        }

        public async Task<CompanyBranchUpdateResponseDto> UpdateCompanyBranch(CompanyBranchUpdateRequestDto dto)
        {
            var companyBranchEntity = _mapper.Map<CompanyBranchUpdateRequestEntity>(dto);
            var updatedCompanyBranch = await _companyBranchRepository.UpdateCompanyBranch(companyBranchEntity);
            var response = _mapper.Map<CompanyBranchUpdateResponseDto>(updatedCompanyBranch);
            return response;
        }

        public async Task<CompanyBranchDeleteResponseDto?> DeleteCompanyBranch(CompanyBranchDeleteRequestDto dto)
        {
            var companyBranchEntity = _mapper.Map<CompanyBranchDeleteRequestEntity>(dto);
            var result = await _companyBranchRepository.DeleteCompanyBranch(companyBranchEntity);

            if (result == -1)
            {
                return null;
            }

            var responseEntity = new CompanyBranchDeleteResponseEntity { CompanyBranchId = companyBranchEntity.CompanyBranchId };
            var responseDto = _mapper.Map<CompanyBranchDeleteResponseDto>(responseEntity);
            return responseDto;
        }
    }
}
