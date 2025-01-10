using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Entities.Address.AddressType.AddressTypeRequestEntities
{
    public class AddressTypeUpdateRequestEntity
    {
        public int AddressTypeId
        {
            get; set;
        }
        public string AddressTypeName { get; set; } = string.Empty;
  
        public bool IsActive
        {
            get; set;
        }
        public bool IsDelete
        {
            get; set;
        }

        public int UpdatedBy
        {
            get; set;
        }
    }
}
