using OpenAI_API;
using OpenAI_API.Chat;

namespace GPTAgents{
    public class GoldfishAgent{
        public string Personality { get; set; }
        public string Name { get; set; }
        public OpenAIAPI gptApi { get; set; }
        public bool RememberHistory {get; set;}

        public List<ChatMessage> History {get; set;}


        public GoldfishAgent(OpenAIAPI apiWrapper, string name,string personality, bool rememberChatHistory = false)
        {
            gptApi = apiWrapper;
            Name = name;
            Personality = personality;
            RememberHistory = rememberChatHistory;
        }


        public async Task<string?> generateResponseAsync(string message){
            
            var messages = new List<ChatMessage>();

            messages.AddRange(History);

            var newMessage = new ChatMessage(ChatMessageRole.User, message);

            messages.Add(newMessage);
            
            
            

            var result = await gptApi.Chat.CreateChatCompletionAsync(new OpenAI_API.Chat.ChatRequest(){
                Model = "gpt-3.5-turbo",
                Messages =  History
                

            });

            var responseMessage = result?.Choices?.FirstOrDefault()?.Message;

            if(RememberHistory)
                History.Add(newMessage);
                if(responseMessage != null)
                    History.Add(responseMessage);

            return responseMessage?.Content ?? "Error: No response";
        }
    }
}