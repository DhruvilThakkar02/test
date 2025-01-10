namespace HRMS.Dtos.CompanyBranch.CompanyBranchRequestDtos
{
    public class CompanyBranchReadRequestDto
    {
        public int? CompanyBranchId { get; set; }
        public string CompanyBranchName { get; set; } = String.Empty;
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public int? AddressId { get; set; }
    }
}
