namespace HRMS.Dtos.Address.State.StateResponseDtos
{
    public class StateUpdateResponseDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public string UpdatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
