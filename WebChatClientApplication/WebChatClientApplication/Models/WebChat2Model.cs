using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebChatClientApplication.Models
{
    public class WebChat2Model
    {
        public WebChat2Model()
        {
            Task.Run(async () => await SetToken()).Wait();
        }

        public string Token { get; set; }

        private async Task SetToken()
        {
            var secret = ConfigurationManager.AppSettings["ClientDirectLineSecret"];
            var endpoint = ConfigurationManager.AppSettings["ClientDirectLineEndpoint"];

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{endpoint}/tokens/generate");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", secret);
            request.Content = new StringContent(JsonConvert.SerializeObject(new { TrustedOrigins = new string[] { "localhost:4208", "hostdomain.something.com" } }),
                                    Encoding.UTF8,
                                    "application/json");

            var response = await client.SendAsync(request);
            string token = String.Empty;

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<DirectLineToken>(body).Token;

                Token = token;
            }
        }

        private class DirectLineToken
        {
            public string ConversationId { get; set; }
            public string Token { get; set; }
            public short ExpiresIn { get; set; }
        }
    }
}