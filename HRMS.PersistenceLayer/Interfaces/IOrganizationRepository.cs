using HRMS.Entities.Tenant.Organization.OrganizationRequestEntities;
using HRMS.Entities.Tenant.Organization.OrganizationResponseEntities;
using HRMS.Entities.User.User.UserResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrganizationReadResponseEntity>> GetOrganizations();
        Task<OrganizationReadResponseEntity?> GetOrganizationById(int? id);
        Task<OrganizationCreateResponseEntity> CreateOrganization(OrganizationCreateRequestEntity organization);
        Task<OrganizationUpdateResponseEntity?> UpdateOrganization(OrganizationUpdateRequestEntity organization);
        Task<int> DeleteOrganization(OrganizationDeleteRequestEntity organization);
    }
}
