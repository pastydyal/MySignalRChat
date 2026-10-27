
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/", () => "SignalR Chat Server is running");
app.MapHub<SignalR_Example.Server.ChatHub>("/chat");

app.Run();
