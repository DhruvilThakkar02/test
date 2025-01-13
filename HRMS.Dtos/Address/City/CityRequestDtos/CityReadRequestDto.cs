namespace HRMS.Dtos.Address.City.CityRequestDtos
{
    public class CityReadRequestDto
    {
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public bool? IsActive { get; set; }
    }
}
