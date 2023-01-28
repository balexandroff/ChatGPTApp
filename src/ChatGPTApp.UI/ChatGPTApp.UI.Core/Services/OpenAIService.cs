using ChatGPTApp.UI.Core.Interfaces;
using OpenAI_API;

namespace ChatGPTApp.UI.Core.Services
{
    public class OpenAIService : IOpenAIService
    {
        public OpenAIService() 
        {

        }

        public async IAsyncEnumerable<CompletionResult> CompleteText(string request)//byte type
        {
            OpenAIAPI api = new OpenAIAPI("YOUR-API-HERE");

            await foreach (var token in api.Completions.StreamCompletionEnumerableAsync(new CompletionRequest(request, Model.DavinciText, 200, 0.5, presencePenalty: 0.1, frequencyPenalty: 0.1)))
            {
                yield return token;
            }
        }
    }
}
