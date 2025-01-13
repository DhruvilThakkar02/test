using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utility.Helpers.SqlHelpers.Address
{
    public static class AddressTypeStoredProcedures
    {
        public const string GetAddressTypes = "spAddressTypeGetAll";
        public const string GetAddressTypeById = "spAddressTypeGet";
        public const string CreateAddressType = "spAddressTypeAdd";
        public const string UpdateAddressType = "spAddressTypeUpdate";
        public const string DeleteAddressType = "spAddressTypeDelete";
    }
}
