namespace HRMS.Entities.Address.State.StateRequestEntities
{
    public class StateReadRequestEntity
    {
        public int? StateId { get; set; }
        public string? StateName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
