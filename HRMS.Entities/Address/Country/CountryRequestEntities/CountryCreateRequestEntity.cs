namespace HRMS.Entities.Address.Country.CountryRequestEntities
{
    public class CountryCreateRequestEntity
    {
        public string CountryName { get; set; } = String.Empty;
        public int CreatedBy { get; set; } 
        public bool IsActive { get; set; }
    }
}
