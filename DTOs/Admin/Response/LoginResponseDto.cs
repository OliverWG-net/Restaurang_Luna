namespace Restaurang_luna.DTOs.Admin.Response
{
    public class LoginResponseDto
    {
        public bool LoginSuccess { get; set; }
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
