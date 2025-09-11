using Restaurang_luna.DTOs.Admin.Request;
using Restaurang_luna.DTOs.Admin.Response;

namespace Restaurang_luna.ServiceInterface.Auth
{
    public interface IAuthService
    {
        public Task<LoginResponseDto> Authenticate(LoginRequestDto request, CancellationToken ct);
        public Task Logout(CancellationToken ct = default);
    }
}
