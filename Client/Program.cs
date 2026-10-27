using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR.Client;

Console.Write("Введите ваше имя: ");
var userName = Console.ReadLine();

if (string.IsNullOrWhiteSpace(userName))
{
    userName = "Аноним";
}

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5192/chat")
    .WithAutomaticReconnect()
    .Build();

connection.On<string, string>("Receive", async (sender, message) =>
{
    await Console.Out.WriteLineAsync(
        $"\n[{sender}]: {message}");
    await Console.Out.WriteAsync($"\nВы: ");
});

await connection.StartAsync();

Console.WriteLine("Вы подключились к чату!");
Console.WriteLine("Для выхода введите /exit.");

while (true)
{
    Console.Write("\nВы: ");
    var message = Console.ReadLine();

    if (message == null ||
        message.Equals("/exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (string.IsNullOrWhiteSpace(message))
    {
        continue;
    }

    await connection.InvokeAsync("Send", userName, message);
}

await connection.DisposeAsync();
