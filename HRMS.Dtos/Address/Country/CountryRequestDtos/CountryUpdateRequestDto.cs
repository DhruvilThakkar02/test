namespace HRMS.Dtos.Address.Country.CountryRequestDtos
{
    public class CountryUpdateRequestDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = String.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
