
// para rodar o compose
// docker-compose up -d

// criar tópico
/* 
docker exec broker kafka-topics --bootstrap-server broker:9092 --create --topic quickstart
*/

using MesssageApacheKafka;

Task.Run(() => new Receiver().Receive());

new Sender().Send();

Console.ReadLine();