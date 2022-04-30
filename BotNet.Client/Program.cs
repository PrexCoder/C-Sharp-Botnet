using BotNet.Client.Connection;

var sigHub = new SigHub();
await sigHub.StartConnection();
Console.ReadLine();