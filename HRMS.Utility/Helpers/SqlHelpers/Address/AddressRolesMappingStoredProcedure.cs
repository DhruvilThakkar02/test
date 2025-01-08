using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utility.Helpers.SqlHelpers.Address
{
   public static class AddressStoredProcedures
    {
        
            public const string GetAddresses = "spAddressGetAll";
            public const string GetAddressById = "spAddressGet";
            public const string CreateAddress = "spAddressAdd";
            public const string UpdateAddress = "spAddressUpdate";
            public const string DeleteAddress = "spAddressDelete";
        }
    }

