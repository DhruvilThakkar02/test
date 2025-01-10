using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Dtos.Address.AddressType.AddressTypeRequestDtos
{
    public class AddressTypeCreateRequestDto
    {
        public string AddressTypeName { get; set; } = string.Empty;
      

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
