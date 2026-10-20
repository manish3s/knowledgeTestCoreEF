using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KnowledgeTestCore.ApiArea.Models;

namespace KnowledgeTestCore.ApiArea
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAPIController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TestAPIController> _logger;

        public TestAPIController(IHttpClientFactory httpClientFactory, ILogger<TestAPIController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // Added By Manish 
        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "For Test", Data = DateTime.UtcNow });
        }



        // Example: GET /api/testapi/fetch-external?url=https://api.github.com/
        [HttpGet("fetch-external")]
        public async Task<IActionResult> FetchExternal([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return BadRequest("Missing 'url' query parameter.");

            try
            {
                var client = _httpClientFactory.CreateClient();
                // allow callers to pass full url including scheme
                var resp = await client.GetAsync(url);
                var content = await resp.Content.ReadAsStringAsync();
                var mediaType = resp.Content.Headers.ContentType?.MediaType ?? "application/json";
                return Content(content, mediaType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FetchExternal failed for {Url}", url);
                return StatusCode(StatusCodes.Status502BadGateway, $"Error fetching {url}: {ex.Message}");
            }
        }

        // Example POST body: { "targetUrl":"https://example.com/endpoint", "payload":"{\"name\":\"abc\"}" }
        [HttpPost("forward")]
        public async Task<IActionResult> Forward([FromBody] ForwardRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.TargetUrl))
                return BadRequest("Request body must include 'targetUrl'.");

            try
            {
                var client = _httpClientFactory.CreateClient();
                var contentString = request.Payload ?? string.Empty;
                var mediaType = string.IsNullOrWhiteSpace(request.ContentType) ? "application/json" : request.ContentType;
                var content = new StringContent(contentString, Encoding.UTF8, mediaType);
                var resp = await client.PostAsync(request.TargetUrl, content);
                var respContent = await resp.Content.ReadAsStringAsync();
                var respMedia = resp.Content.Headers.ContentType?.MediaType ?? "application/json";
                return Content(respContent, respMedia, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Forward failed to {TargetUrl}", request?.TargetUrl);
                return StatusCode(StatusCodes.Status502BadGateway, $"Error forwarding to {request?.TargetUrl}: {ex.Message}");
            }
        }
    }
}
