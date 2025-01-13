using HRMS.Entities.Address.AddressType.AddressTypeRequestEntities;
using HRMS.Entities.Address.AddressType.AddressTypeResponseEntities;
using HRMS.Entities.User.User.UserRequestEntities;
using HRMS.Entities.User.User.UserResponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IAddressTypeRepository
    {
        Task<IEnumerable<AddressTypeReadResponseEntity>> GetAddressTypes();
        Task<AddressTypeReadResponseEntity?> GetAddressTypeById(int? addresstypeId);
        Task<AddressTypeCreateResponseEntity> CreateAddressType(AddressTypeCreateRequestEntity addresstype);
        Task<AddressTypeUpdateResponseEntity?> UpdateAddressType(AddressTypeUpdateRequestEntity addresstype);
        Task<int> DeleteAddressType(AddressTypeDeleteRequestEntity addresstype);
    }
}
