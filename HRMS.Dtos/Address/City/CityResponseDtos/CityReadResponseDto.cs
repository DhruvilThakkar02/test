﻿namespace HRMS.Dtos.Address.City.CityResponseDtos
{
    public class CityReadResponseDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
