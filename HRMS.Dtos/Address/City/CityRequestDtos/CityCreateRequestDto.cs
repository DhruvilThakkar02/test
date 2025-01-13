namespace HRMS.Dtos.Address.City.CityRequestDtos
{
    public class CityCreateRequestDto
    {
        public string CityName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
