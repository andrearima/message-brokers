

using MessageAWS_SQS;

Task.Run(async () => await new Receiver().Listen());

await new Sender().Send();


Console.ReadLine();