using AutoMapper;
using HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos;
using HRMS.Dtos.CompanyBranch.CompanyBranchResponseDtos;
using HRMS.Entities.CompanyBranch.CompanyBranchRequestEntities;
using HRMS.Entities.CompanyBranch.CompanyBranchResponseEntities;

namespace HRMS.Utility.AutoMapperProfiles.CompanyBranch.CompanyBranchMapping
{
    public class CompanyBranchMappingProfile : Profile
    {
        public CompanyBranchMappingProfile()
        {
            CreateMap<CompanyBranchCreateRequestDto, CompanyBranchCreateRequestEntity>();
            CreateMap<CompanyBranchReadRequestDto, CompanyBranchReadRequestEntity>();
            CreateMap<CompanyBranchUpdateRequestDto, CompanyBranchUpdateRequestEntity>();
            CreateMap<CompanyBranchDeleteRequestDto, CompanyBranchDeleteRequestEntity>();

            CreateMap<CompanyBranchCreateRequestEntity, CompanyBranchCreateResponseEntity>();
            CreateMap<CompanyBranchReadRequestEntity, CompanyBranchReadResponseEntity>();
            CreateMap<CompanyBranchUpdateRequestEntity, CompanyBranchUpdateResponseEntity>();
            CreateMap<CompanyBranchDeleteRequestEntity, CompanyBranchDeleteResponseEntity>();

            CreateMap<CompanyBranchCreateResponseEntity, CompanyBranchCreateResponseDto>();
            CreateMap<CompanyBranchReadResponseEntity, CompanyBranchReadResponseDto>();
            CreateMap<CompanyBranchUpdateResponseEntity, CompanyBranchUpdateResponseDto>();
            CreateMap<CompanyBranchDeleteResponseEntity, CompanyBranchDeleteResponseDto>();
        }
    }
}
