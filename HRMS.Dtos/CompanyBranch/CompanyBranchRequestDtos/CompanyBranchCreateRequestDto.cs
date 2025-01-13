namespace HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos
{
    public class CompanyBranchCreateRequestDto
    {
        public string CompanyBranchName { get; set; } = String.Empty;
        public string CompanyBranchHead { get; set; } = String.Empty;
        public string ContactNumber { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
