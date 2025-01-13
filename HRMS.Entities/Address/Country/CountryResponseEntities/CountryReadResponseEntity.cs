namespace HRMS.Entities.Address.Country.CountryResponseEntities
{
    public class CountryReadResponseEntity
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; } 
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
