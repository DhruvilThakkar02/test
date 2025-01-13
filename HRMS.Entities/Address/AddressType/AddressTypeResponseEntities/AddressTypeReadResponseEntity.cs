using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Entities.Address.AddressType.AddressTypeResponseEntities
{
    public class AddressTypeReadResponseEntity
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
