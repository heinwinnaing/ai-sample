using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

try 
{
    //docker run -d -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama:latest
    //docker exec -it ollama ollama run llama3

    var builder = Host.CreateApplicationBuilder();
    builder.Services
        .AddChatClient(new OllamaChatClient(endpoint: new Uri("http://localhost:11434"), modelId: "llama3"));

    var app = builder.Build();
    var chatClient = app.Services.GetRequiredService<IChatClient>();
    
    bool chatting = true;
    List<ChatMessage> messages = new();
    while (chatting)
    {
        Console.ForegroundColor = ConsoleColor.Yellow ;
        Console.Write($"Prompt: ");
        var input = Console.ReadLine()!;
        messages.Add(new ChatMessage(ChatRole.User, input));

        Console.ForegroundColor = ConsoleColor.Green;
        var response = await chatClient.GetResponseAsync(messages);
        await foreach(var result in chatClient.GetStreamingResponseAsync(new ChatMessage(ChatRole.User, input)))
        {
            Console.Write(result.Text);
        }
        Console.WriteLine(Environment.NewLine);
    }
}
catch(Exception ex) 
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"{ex.Message} {ex.InnerException?.Message}");
}