using AutoMapper;
using HRMS.BusinessLayer.Interfaces;
using HRMS.Dtos.User.Login.LoginRequestDtos;
using HRMS.Dtos.User.Login.LoginResponseDtos;
using HRMS.Entities.User.Login.LoginRequestEntities;
using HRMS.PersistenceLayer.Interfaces;

namespace HRMS.BusinessLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        public LoginService(IMapper mapper, ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var response = _mapper.Map<LoginRequestEntity>(request);
            var loginResponse = await _loginRepository.Login(response,"secretkey");
            return _mapper.Map<LoginResponseDto>(loginResponse);
        }
    }
}
