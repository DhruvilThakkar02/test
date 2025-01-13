namespace HRMS.Dtos.Address.State.StateRequestDtos
{
    public class StateCreateRequestDto
    {
        public string StateName { get; set; } = String.Empty;
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
