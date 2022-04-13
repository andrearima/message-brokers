using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageRabbitMQ
{
    internal class Sender
    {
        public void Send()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine("Digite a mensagem ou S para sair: ");
                var message = Console.ReadLine();

                while (true)
                {   
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"{DateTime.Now} Msg Enviada: {message}");

                    Console.WriteLine("Digite a mensagem ou S para sair: ");
                    message = Console.ReadLine();
                    if (message == "S") break;
                }
            }
            
        }
    }
}
