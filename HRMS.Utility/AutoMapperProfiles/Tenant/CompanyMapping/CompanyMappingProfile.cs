using AutoMapper;
using HRMS.Dtos.Tenant.Company.CompanyRequestDtos;
using HRMS.Dtos.Tenant.Company.CompanyResponseDtos;
using HRMS.Entities.Tenant.Company.CompanyRequestEntities;
using HRMS.Entities.Tenant.Company.CompanyResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.Tenant.CompanyMapping
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<CompanyCreateRequestDto, CompanyCreateRequestEntity>();
            CreateMap<CompanyReadRequestDto, CompanyReadRequestEntity>();
            CreateMap<CompanyDeleteRequestDto, CompanyDeleteRequestEntity>();
            CreateMap<CompanyUpdateRequestDto, CompanyUpdateRequestEntity>();


            CreateMap<CompanyCreateRequestEntity, CompanyCreateResponseEntity>();
            CreateMap<CompanyReadRequestEntity, CompanyReadResponseEntity>();
            CreateMap<CompanyDeleteRequestEntity, CompanyDeleteResponseEntity>();
            CreateMap<CompanyUpdateRequestEntity, CompanyUpdateResponseEntity>();


            CreateMap<CompanyCreateResponseEntity, CompanyCreateResponseDto>();
            CreateMap<CompanyReadResponseEntity, CompanyReadResponseDto>();
            CreateMap<CompanyDeleteResponseEntity, CompanyDeleteResponseDto>();
            CreateMap<CompanyUpdateResponseEntity, CompanyUpdateResponseDto>();


        }
    }
}
