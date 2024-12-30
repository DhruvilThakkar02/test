﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Entities.Subdomain.Subdomain.SubdomainRequestEntites
{
    public class SubdomainUpdateRequestEntity
    {
        public int SubdomainID
        {
            get; set;
        }
        public int DomainID
        {
            get; set;
        }
        public string SubdomainName { get; set; } = string.Empty;

        public int UpdatedBy
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
