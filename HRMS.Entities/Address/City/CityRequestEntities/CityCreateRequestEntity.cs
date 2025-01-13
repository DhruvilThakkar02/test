namespace HRMS.Entities.Address.City.CityRequestEntities
{
    public class CityCreateRequestEntity
    {
        public string CityName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
