using HRMS.Entities.Address.Address.AddressRequestEntities;
using HRMS.Entities.Address.Address.AddressResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressReadResponseEntity>> GetAddresses();
        Task<AddressReadResponseEntity?> GetAddressById(int? addressId);
        Task<AddressCreateResponseEntity> CreateAddress(AddressCreateRequestEntity address);
        Task<AddressUpdateResponseEntity?> UpdateAddress(AddressUpdateRequestEntity address);
        Task<int> DeleteAddress(AddressDeleteRequestEntity address);
    }
}
