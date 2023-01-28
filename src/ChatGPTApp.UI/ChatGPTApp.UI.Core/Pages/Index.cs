using ChatGPTApp.UI.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenAI_API;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatGPTApp.UI.Core.Pages;

public partial class Index
{
    [Inject]
    public IOpenAIService _service { get; set; } = default!;

    private string _search = string.Empty;
    private string _error = string.Empty;

    private bool _isLoading = false;

    private List<(DateTime time, string author, List<object> item)> _conversation = new List<(DateTime time, string author, List<object> item)>();

    public Index()
    {

    }

    protected override async Task OnInitializedAsync()
    {
        _isLoading = false;
        _error = string.Empty;
        _search = string.Empty;
        _conversation = new List<(DateTime author, string name, List<object> item)>();

        //try
        //{
        //    while (await _results.MoveNextAsync()) Console.Write(_results.Current + " ");
        //}
        //finally { if (_results != null) await _results.DisposeAsync(); }
    }

    protected async Task Process(string request)
    {
        if (!string.IsNullOrEmpty(request))
        {
            _isLoading = true;

            int timeout = 1000;
            var task = Task.Run(async () =>
            {
               // _conversation.Add((DateTime.Now, "Me", new List<object>() { request }));

                List<object> result = new List<object>();
                await foreach (var item in _service.CompleteText(request))
                {
                    result.Add(item);
                }

                _conversation.AddRange(new List<(DateTime time, string author, List<object> item)>
                {
                    (DateTime.Now, "Me", new List<object>() { request }),
                    (DateTime.Now, "Bot", result)
                });

                _search = string.Empty;
                _isLoading = false;
                _error = string.Empty;
            });

            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                //_search = string.Empty;
                //_isLoading = false;
                //_error = string.Empty;
            }
            else
            {
                _isLoading = false;
                _error = "The Bot is busy right now. Please try again later...";
                //throw new Exception("Timed out. Please try again...");
            }
        }
    }

    protected async Task ClearConversation()
    {
        _isLoading = true;
        _search= string.Empty;
        _conversation.Clear();
        _isLoading = false;
    }

    private async Task HandleOnChange(ChangeEventArgs args)
    {
        _search = args.Value.ToString();
    }
    public async void OnEnter(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await this.Process(_search);
        }
    }

}
