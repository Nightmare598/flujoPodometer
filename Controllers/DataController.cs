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
        string connectionString = "Endpoint=sb://queuepodometer.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=l3MR/PLAWz+VPg0cxR3dyPqQgonjPpioGMrYmtSg67k=;EntityPath=cola1";
        string queueName = "cola1";
        //string mensaje = JsonConvert.SerializeObject(data);
        [HttpPost]
        public async Task<string> PostAsync([FromBody] Data data)
        {
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(JsonConvert.SerializeObject(data));

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return "Mensaje enviado con exito";
        }
    }
}
