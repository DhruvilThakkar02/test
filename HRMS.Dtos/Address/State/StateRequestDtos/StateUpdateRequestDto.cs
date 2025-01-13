namespace HRMS.Dtos.Address.State.StateRequestDtos
{
    public class StateUpdateRequestDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
