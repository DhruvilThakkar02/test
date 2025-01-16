namespace HRMS.Entities.CompanyBranch.CompanyBranchRequestEntities
{
    public class CompanyBranchCreateRequestEntity
    {
        public string CompanyBranchName { get; set; } = String.Empty;
        public string CompanyBranchHead { get; set; } = String.Empty;
        public string ContactNumber { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
