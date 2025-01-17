﻿namespace HRMS.Dtos.Tenant.Tenant.TenantResponseDtos
{
    public class TenantCreateResponseDtos
    {   
        public int TenantId { get; set; }
        public int OrganizationId { get; set; }
        public int DomainId { get; set; }
        public int SubdomainId { get; set; }
        public string? TenantName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
