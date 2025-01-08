using HRMS.Dtos.User.Login.LoginRequestDtos;
using HRMS.Dtos.User.Login.LoginResponseDtos;

namespace HRMS.BusinessLayer.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
