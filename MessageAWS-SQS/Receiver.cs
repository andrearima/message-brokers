using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAWS_SQS
{
    internal class Receiver
    {
        private readonly Configuracao _conf = new();
        private readonly AmazonSQSClient _sqsClient;
        public CancellationTokenSource _source;
        public bool _isPolling;
        public int _delay => 0;
        public Receiver()
        {
            _sqsClient = new AmazonSQSClient(_conf.Credentials);
            //Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPressHandler);
        }

        public async Task Listen()
        {
            _isPolling = true;

            int i = 0;
            try
            {
                _source = new CancellationTokenSource();
                //_token = _source.Token;

                while (_isPolling)
                {
                    i++;
                    Console.Write(i + ": ");
                    await FetchFromQueue();
                    Thread.Sleep(_delay);
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("Application Terminated: " + ex.Message);
            }
            finally
            {
                _source.Dispose();
            }
        }

        private async Task FetchFromQueue()
        {

            ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
            receiveMessageRequest.QueueUrl = _conf.Url;
            receiveMessageRequest.MaxNumberOfMessages = _conf.MaxNumberOfMessages;
            receiveMessageRequest.WaitTimeSeconds = _conf.MessageWaitTimeSeconds;
            ReceiveMessageResponse receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);

            if (receiveMessageResponse.Messages.Count != 0)
            {
                for (int i = 0; i < receiveMessageResponse.Messages.Count; i++)
                {
                    string messageBody = receiveMessageResponse.Messages[i].Body;

                    Console.WriteLine("Message Received: " + messageBody);

                    await DeleteMessageAsync(receiveMessageResponse.Messages[i].ReceiptHandle);
                }
            }
            else
            {
                Console.WriteLine("No Messages to process");
            }
        }
        private async Task DeleteMessageAsync(string recieptHandle)
        {

            DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest();
            deleteMessageRequest.QueueUrl = _conf.Url;
            deleteMessageRequest.ReceiptHandle = recieptHandle;

            DeleteMessageResponse response = await _sqsClient.DeleteMessageAsync(deleteMessageRequest);

        }
    }
}
