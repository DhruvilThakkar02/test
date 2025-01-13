namespace HRMS.Dtos.Address.City.CityRequestDtos
{
    public class CityUpdateRequestDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
