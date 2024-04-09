using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private static HubConnection _connection;

        [HttpGet("ConnectToServer")]
        public async Task<IActionResult> ConnectToServer()
        {
            if (_connection != null)
                return Ok();

            var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7251/notify")
            .Build();

            connection.On<int, string, decimal>("ReceiveNotification", (productId, productName, totalPrice) =>
            {
                Console.WriteLine($"Received order data\n{productId}: {productName} - {totalPrice}");
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connected to SignalR API.");
                _connection = connection;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
            return Ok();
        }

        [HttpGet("SubscribeToNotifications")]
        public async Task<IActionResult> SubscribeToNotifications(int id)
        {
            if (_connection == null)
            {
                await ConnectToServer();
                await _connection.InvokeAsync("SubscribeToNotifications", $"product{id}Group");
            }
            else
                await _connection.InvokeAsync("SubscribeToNotifications", $"product{id}Group");

            return Ok();
        }
    }
}
