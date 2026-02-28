using static KnowledgeTestCore.DTOs.AuthDto;

namespace KnowledgeTestCore.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    }
}
