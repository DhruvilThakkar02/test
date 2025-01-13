using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Entities.Address.AddressType.AddressTypeRequestEntities
{
    public class AddressTypeCreateRequestEntity
    {
        public string AddressTypeName
        {
            get; set;
        }
       
        

        public bool IsActive
        {
            get; set;
        }

        public int CreatedBy
        {
            get; set;
        }
    }
}
