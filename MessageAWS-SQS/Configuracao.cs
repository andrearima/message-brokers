using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAWS_SQS
{
    internal class Configuracao
    {
        private string _region = "us-east-1";
        public string NomeFila => "TesteSQS";
        public string Arn => "arn:aws:sqs:us-east-1:114141527404:TesteSQS";
        public string Url => "https://sqs.us-east-1.amazonaws.com/114141527404/TesteSQS";
        public int MessageWaitTimeSeconds => 20;
        public int MaxNumberOfMessages => 10;
        public AmazonSQSClient _sqsClient;
        public BasicAWSCredentials Credentials
        {
            get
            {
                var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddUserSecrets<Configuracao>();

                IConfigurationRoot configuration = builder.Build();
                // necessário criar Secret User no padrão ou adicionar no appsettings
                /*
                   {
                      "AWS": {
                        "AccessKey": "accessKey",
                        "SecretKey": "secret"
                      }
                    }
                 */
                var access = configuration.GetSection("AWS:AccessKey").Value;
                var secret = configuration.GetSection("AWS:SecretKey").Value;

                return new BasicAWSCredentials(access, secret);
            }
        }
        public RegionEndpoint Region
        {
            get
            {
                return RegionEndpoint.GetBySystemName(_region);
            }
        }
    }
}
