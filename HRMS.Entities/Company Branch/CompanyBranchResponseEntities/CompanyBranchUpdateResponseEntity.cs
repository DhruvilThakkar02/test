namespace HRMS.Entities.CompanyBranch.CompanyBranchResponseEntities
{
    public class CompanyBranchUpdateResponseEntity
    {
        public int CompanyBranchId { get; set; }
        public string CompanyBranchName { get; set; } = String.Empty;
        public string CompanyBranchHead { get; set; } = String.Empty;
        public string ContactNumber { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public int CompanyId { get; set; }
        public int UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
