using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebChatClientApplication.Models
{
    public class WebChatModel
    {
        public WebChatModel()
        {
            Task.Run(async () => await SetToken()).Wait();
        }

        public string Token { get; set; }

        private async Task SetToken()
        {
            string botChatSecret = ConfigurationManager.AppSettings["WebChatSecret"];

            var request = new HttpRequestMessage(HttpMethod.Get, "https://webchat.botframework.com/api/tokens");
            request.Headers.Add("Authorization", "BOTCONNECTOR " + botChatSecret);

            using (HttpResponseMessage response = await new HttpClient().SendAsync(request))
            {
                string token = await response.Content.ReadAsStringAsync();
                Token = token.Replace("\"", "");
            }
        }
    }
}