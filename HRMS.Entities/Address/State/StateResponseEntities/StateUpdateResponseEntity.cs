namespace HRMS.Entities.Address.State.StateResponseEntities
{
    public class StateUpdateResponseEntity
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
