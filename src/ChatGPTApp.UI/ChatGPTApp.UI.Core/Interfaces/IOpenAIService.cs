using ChatGPTApp.Shared;
using OpenAI_API;

namespace ChatGPTApp.UI.Core.Interfaces
{
    public interface IOpenAIService
    {
        public IAsyncEnumerable<CompletionResult> CompleteText(string request);
    }
}
