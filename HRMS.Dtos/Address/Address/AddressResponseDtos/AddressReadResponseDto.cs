﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Dtos.Address.Address.AddressResponseDtos
{
    public class AddressReadResponseDto
    {
        public int AddressId
        {
            get; set;
        }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public int CityId
        {
            get; set;
        }
        public int StateId
        {
            get; set;
        }
        public int CountryId
        {
            get; set;
        }
        public int PostalCode
        {
            get; set;
        }
        public int AddressTypeId
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
        public int CreatedBy
        {
            get; set;
        }
        public int UpdatedBy
        {
            get; set;
        }

        public DateTime CreatedAt
        {
            get; set;
        }
        public DateTime UpdatedAt
        {
            get; set;
        }
    }
}
