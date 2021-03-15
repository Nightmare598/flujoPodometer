using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using apiProductor.Models;
using Newtonsoft.Json;

namespace apiProductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public async Task<string> PostAsync([FromBody] Data data)
        {
            string connectionString = "Endpoint=sb://queuepodometer.servicebus.windows.net/;SharedAccessKeyName=Escuchar;SharedAccessKey=vItB/2VHDRn/bnv+xSYISzJpbTQm7/W7oJck76Jjn58=;EntityPath=cola1";
            string queueName = "cola1";
            string mensaje = JsonConvert.SerializeObject(data);

            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return "Mensaje enviado con exito";
        }
    }
}
