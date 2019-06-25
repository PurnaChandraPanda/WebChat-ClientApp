using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EchoAuthBot.ClientApp
{
    public class HomeController : Controller
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(
                            HttpMethod.Post,
                            "https://directline.botframework.com/v3/directline/tokens/generate");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["DirectLineSecret"]);

            var userId = $"dl_{Guid.NewGuid()}";

            request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new { User = new { Id = userId } }),
                    Encoding.UTF8,
                    "application/json");

            var response = await _httpClient.SendAsync(request);
            string token = String.Empty;
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<DirectLineToken>(body).token;
            }
            var config = new ChatConfig()
            {
                Token = token,
                UserId = userId
            };
            return View(config);
        }
    }

    public class DirectLineToken
    {
        public string conversationId { get; set; }
        public string token { get; set; }
        public int expires_in { get; set; }
    }
    public class ChatConfig
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}