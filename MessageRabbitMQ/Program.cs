// See https://aka.ms/new-console-template for more information
using MessageRabbitMQ;

// Docker Command
// docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management

Task.Run( () => new Receiver().Receive());
new Sender().Send();
Console.ReadLine();
