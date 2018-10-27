## Client app via "Web Chat" channel
Snippet is in ..\Views\WebChat\index.cshtml

## Client app via "DirectLine" channel
Snippet is in ..\Views\WebChat2\index.cshtml

The recommendation is to ensure secret is secured in either case. 

## Updated logic for speech support

'''
    var speechOptions = {
        speechRecognizer: new CognitiveServices.SpeechRecognizer({
            subscriptionKey: '@ConfigurationManager.AppSettings["bing-speech-api"]',
            locale: 'de-DE'
        }),
        speechSynthesizer: new CognitiveServices.SpeechSynthesizer({
            //gender: CognitiveServices.SyntheisGender.Female,
            subscriptionKey: '@ConfigurationManager.AppSettings["bing-speech-api"]',
            voiceName: 'Microsoft Server Speech Text to Speech Voice (de-DE, Stefan, Apollo)'
        })
    };

    BotChat.App({
        directLine: { secret: '@Model.Token' },
        locale: 'de-DE', // locale defaults to 'en-US'
        user: userDetails,
        botConnection: botConnection,
        speechOptions: speechOptions, // comment speechOptions property if speech is not in concern
        bot: { id: '@ConfigurationManager.AppSettings["BotId"]' },
        resize: 'detect'
    }, document.getElementById("bot"));
    </script>
'''

