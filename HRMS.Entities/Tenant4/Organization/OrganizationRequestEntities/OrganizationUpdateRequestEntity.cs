﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Entities.Tenant.Organization.OrganizationRequestEntities
{
    public class OrganizationUpdateRequestEntity
    {
        public int OrganizationID { get; set; }

        public string OrganizationName { get; set; } = string.Empty;  

        public int UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
