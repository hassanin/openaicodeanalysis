using Azure.AI.OpenAI;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCostAnalyzer.Util
{
    public class OpenAiTrials
    {
        public static string OpenAiEndpoint = "endpoint";
        public static string ApiKey = "api-key";
        public static string DeploymentName = "gpt351";
        public async static Task<string> TestGpt3()
        {
            OpenAIClient client = new OpenAIClient(
        new Uri(OpenAiEndpoint),
        new AzureKeyCredential(ApiKey));
            //var chatMesages = new ChatMessage(ChatRole.User,"generate a C# function that adds two numbers")
            //var ds1 = new ChatCompletionsOptions( { MaxTokens = 500, ChoicesPerPrompt = 1, Messages = new List<ChatMessage>(new[] {chatMesages}) };
            //ds1.
            //client.GetChatCompletionsAsync(DeploymentName)
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
    {
        new ChatMessage(ChatRole.System, "You are a helpful assistant. You know coding you provide useful code samples that compiles"),
        new ChatMessage(ChatRole.User, "generate basic Web Crawler in C# that takes an input list of websites, reads the site links, add the link to a database, check if the link has been visisted, if not, visit the page and start over"),

    },
                //MaxTokens=500,
            };
            var res = await client.GetChatCompletionsAsync(DeploymentName, chatCompletionsOptions);
            var rgg = res.Value.Choices[0].Message.Content;
            Console.WriteLine(rgg);
            return rgg;

        }

    }
}
