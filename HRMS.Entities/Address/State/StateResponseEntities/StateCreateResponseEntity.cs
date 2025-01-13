namespace HRMS.Entities.Address.State.StateResponseEntities
{
    public class StateCreateResponseEntity
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
