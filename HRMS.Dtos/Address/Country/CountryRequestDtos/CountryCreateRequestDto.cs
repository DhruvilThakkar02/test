namespace HRMS.Dtos.Address.Country.CountryRequestDtos
{
    public class CountryCreateRequestDto
    {
        public string CountryName { get; set; } = String.Empty;
        public int CreatedBy { get; set; } 
        public bool IsActive { get; set; }
    }
}
