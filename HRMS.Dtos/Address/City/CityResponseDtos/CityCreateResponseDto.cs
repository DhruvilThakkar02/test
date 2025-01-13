namespace HRMS.Dtos.Address.City.CityResponseDtos
{
    public class CityCreateResponseDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
