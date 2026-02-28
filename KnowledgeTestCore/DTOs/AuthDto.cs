namespace KnowledgeTestCore.DTOs
{
    public class AuthDto
    {
        public record LoginRequest(string Username, string Password);

        public record RegisterRequest(string Username, string Password, string Role);

        public record AuthResponse(string Token, string Username, string Role);
    }
}
