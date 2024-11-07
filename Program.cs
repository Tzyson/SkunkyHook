using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebhookSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Write("   _____ _                _          _    _             _    \r\n  / ____| |              | |        | |  | |           | |   \r\n | (___ | | ___   _ _ __ | | ___   _| |__| | ___   ___ | | __\r\n  \\___ \\| |/ / | | | '_ \\| |/ / | | |  __  |/ _ \\ / _ \\| |/ /\r\n  ____) |   <| |_| | | | |   <| |_| | |  | | (_) | (_) |   < \r\n |_____/|_|\\_\\\\__,_|_| |_|_|\\_\\\\__, |_|  |_|\\___/ \\___/|_|\\_\\\r\n                                __/ |                        \r\n                               |___/                        By Tzy\nJOIN AXYS! https://discord.gg/sBaXpWZGGb\n");

                Console.Write("Webhook URL: ");
                string webhookUrl = Console.ReadLine();
                Console.Write("Message: ");
                string message = Console.ReadLine();

                await SendWebhookMessage(webhookUrl, message);

                // Wait 3 seconds before clearing and starting again
                await Task.Delay(1000);
                Console.Clear();
            }
        }

        private static async Task SendWebhookMessage(string webhookUrl, string message)
        {
            using (HttpClient client = new HttpClient())
            {
                var payload = new
                {
                    content = message
                };

                var jsonPayload = new StringContent(
                    JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                try
                {
                    HttpResponseMessage response = await client.PostAsync(webhookUrl, jsonPayload);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send message. Status Code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
