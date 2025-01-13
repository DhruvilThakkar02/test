namespace HRMS.Entities.Address.City.CityRequestEntities
{
    public class CityUpdateRequestEntity
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = String.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
