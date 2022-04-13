using Confluent.Kafka;

namespace MesssageApacheKafka
{
    internal class Receiver
    {
        public void Receive()
        {

            string bootstrapServers = "localhost:9092";
            string nomeTopic = "quickstart";
            string groupId = "1";

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(nomeTopic);

                    try
                    {
                        while (true)
                        {
                            var cr = consumer.Consume(cts.Token);
                            Console.WriteLine($"Mensagem lida: {cr.Message.Value}");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
