using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.TenancyRole.TenancyRoleRequestDtos;
using HRMS.Dtos.Tenant.TenancyRole.TenancyRoleResponseDtos;
using HRMS.Entities.Tenant.TenancyRole.TenancyRoleRequestEntities;
using HRMS.Entities.Tenant.TenancyRole.TenancyRoleResponseEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class TenancyRoleService : ITenancyRoleService
    {
        private readonly ITenancyRoleRepository _tenancyroleRepository;
        private readonly IMapper _mapper;

        public TenancyRoleService(ITenancyRoleRepository tenancyRepository, IMapper mapper)
        {
            _tenancyroleRepository = tenancyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TenancyRoleReadResponseDto>> GetTenancyRoles()
        {
            var roles = await _tenancyroleRepository.GetTenancyRoles();

            var response = _mapper.Map<IEnumerable<TenancyRoleReadResponseDto>>(roles);
            return response;
        }

        public async Task<TenancyRoleReadResponseDto?> GetTenancyRoleById(int? tenancyroleId)
        {
            var role = await _tenancyroleRepository.GetTenancyRoleById(tenancyroleId);
            if (role == null || role.TenancyRoleId == -1)
            {
                return null;
            }

            var response = _mapper.Map<TenancyRoleReadResponseDto>(role);
            return response;
        }

        public async Task<TenancyRoleCreateResponseDto> CreateTenancyRole(TenancyRoleCreateRequestDto roleDto)
        {
            var tenancyroleEntity = _mapper.Map<TenancyRoleCreateRequestEntity>(roleDto);
            var addedUser = await _tenancyroleRepository.CreateTenancyRole(tenancyroleEntity);
            var response = _mapper.Map<TenancyRoleCreateResponseDto>(addedUser);
            return response;
        }

        public async Task<TenancyRoleUpdateResponseDto> UpdateTenancyRole(TenancyRoleUpdateRequestDto roleDto)
        {
            var teanancyRoleEntity = _mapper.Map<TenancyRoleUpdateRequestEntity>(roleDto);
            var updatedteanancyRole = await _tenancyroleRepository.UpdateTenancyRole(teanancyRoleEntity);
            var response = _mapper.Map<TenancyRoleUpdateResponseDto>(updatedteanancyRole);
            return response;
        }

        public async Task<TenancyRoleDeleteResponseDto?> DeleteTenancyRole(TenancyRoleDeleteRequestDto roleDto)
        {
            var tenancyRoleEntity = _mapper.Map<TenancyRoleDeleteRequestEntity>(roleDto);
            var result = await _tenancyroleRepository.DeleteTenancyRole(tenancyRoleEntity);
            if (result == -1)
            {
                return null;
            }
            var responseEntity = new TenancyRoleDeleteResponseEntity { TenancyRoleId = tenancyRoleEntity.TenancyRoleId };
            var responseDto = _mapper.Map<TenancyRoleDeleteResponseDto>(responseEntity);

            return responseDto;
        }
    }
}
