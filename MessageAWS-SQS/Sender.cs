using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAWS_SQS
{
    internal class Sender
    {
        private readonly Configuracao _conf = new Configuracao();
        private readonly AmazonSQSClient _sqsClient;
        public Sender()
        {
            _sqsClient = new AmazonSQSClient(_conf.Credentials);
        }
        public async Task Send()
        {
            
            try
            {
                var teste = new List<string> { "msg1", "msg2", "msg3", "msg4", "msg5" };
                foreach (var item in teste)
                {
                    var sendMessageRequest = new SendMessageRequest
                    {
                        QueueUrl = _conf.Url,
                        MessageBody = JsonConvert.SerializeObject(item)
                    };


                    await _sqsClient.SendMessageAsync(sendMessageRequest);
                }
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
