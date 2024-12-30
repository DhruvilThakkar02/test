﻿using AutoMapper;
using HRMS.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.Tenant.Organization.OrganizationRequestDtos;
using HRMS.Dtos.Tenant.Organization.OrganizationResponseDtos;
using HRMS.Entities.Tenant.Organization.OrganizationResponseEntities;
using HRMS.Entities.Tenant.Organization.OrganizationRequestEntities;
using HRMS.Entities.User.User.UserRequestEntities;
using HRMS.Dtos.User.User.UserResponseDtos;

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


        public async Task<OrganizationReadResponseDto> GetOrganization(int id)
        {

            var organization = await _organizationRepository.GetOrganization(id);
            if (organization == null)
            {
                return null;
            }


            return _mapper.Map<OrganizationReadResponseDto>(organization);
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
            var responseEntity = new OrganizationDeleteResponseEntity { OrganizationID = organizationEntity.OrganizationID };
            var responseDto = _mapper.Map<OrganizationDeleteResponseDto>(responseEntity);
            return responseDto;
        }

    }
}




