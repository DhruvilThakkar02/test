namespace HRMS.Entities.Address.State.StateRequestEntities
{
    public class StateUpdateRequestEntity
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
