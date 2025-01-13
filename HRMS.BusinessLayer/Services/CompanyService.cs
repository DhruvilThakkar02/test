using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Company.CompanyRequestDtos;
using HRMS.Dtos.Tenant.Company.CompanyResponseDtos;
using HRMS.Dtos.User.User.UserResponseDtos;
using HRMS.Entities.Tenant.Company.CompanyRequestEntities;
using HRMS.Entities.Tenant.Company.CompanyResponseEntities;
using HRMS.Entities.User.User.UserRequestEntities;
using HRMS.PersistenceLayer.Interfaces;
using HRMS.PersistenceLayer.Repositories;

namespace HRMS.BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyReadResponseDto>> GetCompanys()
        {
            var companies = await _companyRepository.GetCompanies();
            var response = _mapper.Map<IEnumerable<CompanyReadResponseDto>>(companies);
            return response;
        }
        public async Task<CompanyReadResponseDto?> GetCompanyById(int? companyId)
        {
            var company = await _companyRepository.GetCompanyById(companyId);
            if (company == null || company.CompanyId == -1)
            {
                return null;
            }
            var response = _mapper.Map<CompanyReadResponseDto>(company);
            return response;
        }
        public async Task<CompanyCreateResponseDto> CreateCompany(CompanyCreateRequestDto dto)
        {
            var companys = _mapper.Map<CompanyCreateRequestEntity>(dto);
            var createdCompany = await _companyRepository.CreateCompany(companys);
            return _mapper.Map<CompanyCreateResponseDto>(createdCompany);
        }
        public async Task<CompanyUpdateResponseDto> UpdateCompany(CompanyUpdateRequestDto companyDto)
        {
            var companyEntity = _mapper.Map<CompanyUpdateRequestEntity>(companyDto);
            var updatedCompany = await _companyRepository.UpdateCompany(companyEntity);
            var response = _mapper.Map<CompanyUpdateResponseDto>(updatedCompany);
            return response;
        }
        public async Task<CompanyDeleteResponseDto?> DeleteCompany(CompanyDeleteRequestDto companyDto)
        {
            var companyEntity = _mapper.Map<CompanyDeleteRequestEntity>(companyDto);
            var result = await _companyRepository.DeleteCompany(companyEntity);
            if (result == -1)
            {
                return null;
            }
            var responseEntity = new CompanyDeleteResponseEntity { CompanyId = companyEntity.CompanyId };
            var responseDto = _mapper.Map<CompanyDeleteResponseDto>(responseEntity);

            return responseDto;
        }
    }
}
