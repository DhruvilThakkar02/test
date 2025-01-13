namespace HRMS.Entities.Tenant.Company.CompanyRequestEntities
{
    public class CompanyUpdateRequestEntity
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string CompanyType { get; set; } = string.Empty;
        public DateTime FoundedDate { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string WebsiteUrl { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string GstNumber { get; set; } = string.Empty;
        public string PfNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? AddressId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
