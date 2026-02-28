using KnowledgeTestCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static KnowledgeTestCore.DTOs.AuthDto;

namespace KnowledgeTestCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _auth.LoginAsync(request);
            return result is not null
                ? Ok(result)
                : Unauthorized(new { message = "Invalid credentials" });
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _auth.RegisterAsync(request);
            return result is not null
                ? Ok(result)
                : BadRequest(new { message = "Username already exists" });
        }
    }
}
