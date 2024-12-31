﻿namespace HRMS.Dtos.Subdomain.Subdomain.SubdomainRequestDto
{
    public class SubdomainCreateRequestDto
    {
        public int DomainID
        {
            get; set;
        }
        public string SubdomainName { get; set; } = string.Empty;
        public int CreatedBy
        {
            get; set;
        }
        public bool IsActive
        {
            get; set;
        }
        public bool IsDelete
        {
            get; set;
        }
    }
}
