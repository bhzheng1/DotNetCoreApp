// See https://aka.ms/new-console-template for more information
using System.Net.WebSockets;
using System.Text;

Console.WriteLine("Hello, World!");
var Connection = "wss://long-short-profit.glitch.me/";
using (var socket = new ClientWebSocket())
{
    socket.Options.SetRequestHeader("user-agent","Mozilla");
    try
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(120));
        await socket.ConnectAsync(new Uri(Connection), cts.Token);
        Console.WriteLine(socket.State);
        
        await Send(socket, "data");
        await Receive(socket, cts.Token);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR - {ex.Message}");
    }
}

static async Task Send(ClientWebSocket socket, string data) =>
    await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true, CancellationToken.None);

static async Task Receive(ClientWebSocket socket, CancellationToken stoppingToken)
{
    var buffer = new ArraySegment<byte>(new byte[2048]);
    while (!stoppingToken.IsCancellationRequested)
    {
        WebSocketReceiveResult result;
        using (var ms = new MemoryStream())
        {
            do
            {
                result = await socket.ReceiveAsync(buffer, stoppingToken);
                ms.Write(buffer.Array, buffer.Offset, result.Count);
            } while (!result.EndOfMessage);

            if (result.MessageType == WebSocketMessageType.Close)
                break;

            ms.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(ms, Encoding.UTF8))
                Console.WriteLine(await reader.ReadToEndAsync());
        }
    };
}
