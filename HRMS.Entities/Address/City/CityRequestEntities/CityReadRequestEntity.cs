namespace HRMS.Entities.Address.City.CityRequestEntities
{
    public class CityReadRequestEntity
    {
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
