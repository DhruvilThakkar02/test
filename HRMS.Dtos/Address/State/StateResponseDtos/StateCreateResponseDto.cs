namespace HRMS.Dtos.Address.State.StateResponseDtos
{
    public class StateCreateResponseDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
