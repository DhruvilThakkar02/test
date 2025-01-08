using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Entities.Address.Address.AddressRequestEntities
{
    public  class AddressCreateRequestEntity
    {
       
        public string AddressLine1 { get; set; } 
        public string AddressLine2 { get; set; } 
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
       
        public int CreatedBy
        {
            get; set;
        }
       

       
    }
}
