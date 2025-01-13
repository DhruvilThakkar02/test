namespace HRMS.Entities.Address.Country.CountryRequestEntities
{
    public class CountryUpdateRequestEntity
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = String.Empty;
        public int UpdatedBy { get; set; } 
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
