# WebChat-ClientApp

Bot Framework echo bot samples in v3 and v4 for Emulator/ DirectLine/ WebChat channels interaction.

The respective discussion is at v3 and v4 folders. I would suggest to visit the v4 folder for latest updates related to Bot SDK and WebChat UI.

## Note

v3 -> stands for v3 Bot framework SDK based app
v4 -> stands for v4 Bot framework SDK based app

## v4 app

Modify IWebHostBuilder to read configured items from `appsettings.Development.json`.
```diff
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
+                 .ConfigureAppConfiguration((hostingContext, config) => {
+                    var env = hostingContext.HostingEnvironment;

+                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
+                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true); // optional extra provider
                })
                .UseStartup<Startup>();
```

Set the stage for middleware in `Startup.cs`.
```diff
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Create the credential provider to be used with the Bot Framework Adapter.
            services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>();

            // Create the Bot Framework Adapter with error handling enabled. 
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the storage we'll be using for User and Conversation state. (Memory is great for testing purposes.) 
+            services.AddSingleton<IStorage, MemoryStorage>();

            // Create the User state. (Used in this bot's Dialog implementation.)
+            services.AddSingleton<UserState>();

            // Create the Conversation state. (Used by the Dialog system itself.)
+            services.AddSingleton<ConversationState>();

            // The Dialog that will be run by the bot.
+            services.AddSingleton<MainDialog>();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
+            services.AddTransient<IBot, AuthBot<MainDialog>>();
        }
```

Waterfall dialog flow steps' configuration in `MainDialog.cs`.
```
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                PromptStepAsync,
                LoginStepAsync,
                DisplayTokenPhase1Async,
                DisplayTokenPhase2Async,
            }));
```

The client side token generation for the View page in `HomeController.cs`.
```diff
	public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(
                            HttpMethod.Post,
                            "https://directline.botframework.com/v3/directline/tokens/generate");
+            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["DirectLineSecret"]);

            var userId = $"dl_{Guid.NewGuid()}";

            request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new { User = new { Id = userId } }),
                    Encoding.UTF8,
                    "application/json");

+            var response = await _httpClient.SendAsync(request);
            string token = String.Empty;
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
+                token = JsonConvert.DeserializeObject<DirectLineToken>(body).token;
            }
            var config = new ChatConfig()
            {
                Token = token,
                UserId = userId
            };
            return View(config);
        }
```

The View page got to be updated to read token from server side in `Home\Index.cshtml`.
```diff
<body>
    <div id="webchat" role="main"></div>    
    <script src="https://cdn.botframework.com/botframework-webchat/master/webchat.js"></script>
    <script>
        // Set the StyleOptions for avatar
        const styleOptions = {
            botAvatarInitials: 'WC',
            userAvatarInitials: 'WW'
        };

        // Render the webchat control
        window.WebChat.renderWebChat({
+            directLine: window.WebChat.createDirectLine({ token: `@Model.Token.ToString()` }),
+            userID: `@Model.UserId.ToString()`,
            username: 'Web Chat User',
            locale: 'en-US',
            styleOptions
        }, document.getElementById('webchat'));
        document.querySelector('#webchat > *').focus();
    </script>
</body>
```