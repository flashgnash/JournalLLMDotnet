using GPTAgents;
using Microsoft.Extensions.Configuration;
using OpenAI_API.Chat;

namespace GPTeepsky{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var key = config.GetSection("GPT").GetValue<string>("apiKey");

            var gptApi = new OpenAI_API.OpenAIAPI(key);

            var agent = new GoldfishAgent(gptApi, "GPTeepsky", "Respond to all prompts future prompts with \"cheese\"");


            Console.WriteLine(await agent.generateResponseAsync("What is the meaining of life, the universe, and everything?"));
        }
    }
}
