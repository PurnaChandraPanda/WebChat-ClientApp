# EchoAuthBot

Bot Framework v4 echo bot sample.

This bot has been created using [Bot Framework](https://dev.botframework.com), it shows how to create a simple bot that accepts input from the user and echoes it back.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) version 2.1

  ```bash
  # determine dotnet version
  dotnet --version
  ```

## To try this sample

- In a terminal, navigate to `EchoAuthBot`

    ```bash
    # change into project folder
    cd # EchoAuthBot
    ```

- Run the bot from a terminal or from Visual Studio, choose option A or B.

  A) From a terminal

  ```bash
  # run the bot
  dotnet run
  ```

  B) Or from Visual Studio

  - Launch Visual Studio
  - File -> Open -> Project/Solution
  - Navigate to `EchoAuthBot` folder
  - Select `EchoAuthBot.csproj` file
  - Press `F5` to run the project

## Create Bot Channels Registration

- Go to Azure portal, create `Bot Channels Registration` resource
- Name the resource/ leave URI blank
- Map it to AAD v2 registration as [here](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-authentication?view=azure-bot-service-4.0&tabs=aadv2%2Ccsharp%2Cbot-oauth#create-and-register-an-azure-ad-application)
- For simplicity, add scopes for `openid`, `profile`, `User.Read`.
- Copy the conection name, appId, appPassword values and update `appsettings.json` or `appsettings.Development.json` (if in development mode)

## Testing the bot using Bot Framework Emulator

[Bot Framework Emulator](https://github.com/microsoft/botframework-emulator) is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

- Install the Bot Framework Emulator version 4.3.0 or greater from [here](https://github.com/Microsoft/BotFramework-Emulator/releases)

### Connect to the bot using Bot Framework Emulator

- Launch Bot Framework Emulator
- Create an ngrok based URI for local testing as [here](https://blog.botframework.com/2017/10/19/debug-channel-locally-using-ngrok/)
- File -> Open Bot
- Enter a Bot URL of `http://<ngrok-forward-uri>/api/messages`
- Enter earlier copied optional appid/ password values.
- You can test the OAuth flow in emulator then.

## Test DirectLine flow

- Create DirectLine channel registration
- Copy the secret key
- Add trusted origin for client side URI to host name (if focus is to by-pass the manual magic code copy) as following
	![DirectLine settings](https://github.com/PurnaChandraPanda/WebChat-ClientApp/blob/master/v4/Media/directline-settings.JPG)
- Controller logic is [here](./ClientApp/HomeController.cs#28)
- View page is [here](./Views/Home/Index.cshtml)


## Deploy the bot to Azure

To learn more about deploying a bot to Azure, see [Deploy your bot to Azure](https://aka.ms/azuredeployment) for a complete list of deployment instructions.
