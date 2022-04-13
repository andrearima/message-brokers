
//name: topicotesteSNS


using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MessageAWS_SNS;

string number = "123";
string message = "Hello at " + DateTime.Now.ToShortTimeString();

var creds = new Configuracao().Credentials;

var client = new AmazonSimpleNotificationServiceClient(credentials: creds, region: Amazon.RegionEndpoint.USEast1);
var request = new PublishRequest
{
    Message = message,
    PhoneNumber = number,
};

try
{
    var response = await client.PublishAsync(request);

    Console.WriteLine("Message sent to " + number + ":");
    Console.WriteLine(message);
}
catch (Exception ex)
{
    Console.WriteLine("Caught exception publishing request:");
    Console.WriteLine(ex.Message);
}