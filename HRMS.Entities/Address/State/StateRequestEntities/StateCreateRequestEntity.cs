namespace HRMS.Entities.Address.State.StateRequestEntities
{
    public class StateCreateRequestEntity
    {
        public string StateName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
