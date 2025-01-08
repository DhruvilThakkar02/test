using HRMS.Entities.User.Login.LoginRequestEntities;
using HRMS.Entities.User.Login.LoginResponseEntities;

namespace HRMS.PersistenceLayer.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginResponseEntity> LoginAsync(LoginRequestEntity request);
    }
}
